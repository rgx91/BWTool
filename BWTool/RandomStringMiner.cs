using NBitcoin;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BWTool
{
    /// <summary>
    /// Parallelized random string miner.
    /// Strategy:
    ///  - Build a HashSet of lookup addresses.
    ///  - Partition the search space by first-character prefix.
    ///  - For each prefix, enumerate all tails of length (L-1) sequentially.
    ///  - Run prefixes in parallel with MaxDegreeOfParallelism = CPU count.
    ///  - Use a single writer thread (BlockingCollection) for "keys.txt" output.
    ///  - Update MinerInfo counters with Interlocked.Increment for thread safety.
    /// </summary>
    class RandomStringMiner : IDisposable
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly CancellationTokenSource cancellationToken;
        private readonly string chars;
        private readonly string path;
        private readonly bool compressed;
        private readonly int countOfSha256Operation;
        private readonly int lengthOfString;

        private HashSet<string> lookupSet;
        private volatile int foundKeyCount;

        private readonly BlockingCollection<string> _foundLines = new BlockingCollection<string>(10_000);
        private Task _writerTask;

        private readonly ParallelOptions _po;

        public RandomStringMiner(
            int lengthOfString,
            string pathOfLookupAddresses,
            bool compressed,
            int countOfSha256Operation,
            string charWorkingSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (lengthOfString < 1) throw new ArgumentOutOfRangeException(nameof(lengthOfString));
            if (string.IsNullOrWhiteSpace(pathOfLookupAddresses)) throw new ArgumentNullException(nameof(pathOfLookupAddresses));
            if (string.IsNullOrWhiteSpace(charWorkingSet)) throw new ArgumentNullException(nameof(charWorkingSet));

            this.lengthOfString = lengthOfString;
            this.chars = string.Join("", charWorkingSet.Distinct());
            this.path = pathOfLookupAddresses;
            this.compressed = compressed;
            this.countOfSha256Operation = countOfSha256Operation;

            cancellationToken = new CancellationTokenSource();
            _po = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
        }

        public void StartRandomStringMiner()
        {
            Mine();
        }

        public void StopRandomStringMiner()
        {
            try { cancellationToken.Cancel(); } catch (ObjectDisposedException) { /* no-op */ }
        }

        public void Dispose()
        {
            try { _foundLines?.Dispose(); } catch { }
            try { cancellationToken?.Dispose(); } catch { }
        }

        private void StartWriter()
        {
            _writerTask = Task.Factory.StartNew(() =>
            {
                using var fs = new FileStream("keys.txt", FileMode.Append, FileAccess.Write, FileShare.Read);
                using var sw = new StreamWriter(fs);
                foreach (var line in _foundLines.GetConsumingEnumerable(cancellationToken.Token))
                {
                    sw.WriteLine(line);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void CompleteWriter()
        {
            try
            {
                _foundLines.CompleteAdding();
                _writerTask?.Wait();
            }
            catch (OperationCanceledException) { }
            catch (AggregateException ae) when (ae.InnerExceptions.All(e => e is OperationCanceledException)) { }
        }

        private void Mine()
        {
            MinerInfo.minerStillRunning = true;
            stopwatch.Start();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    // Report combinations
                    MinerInfo.lengthOfJob = ComputeCombinations();
                    MinerInfo.minerThreadInfo = $"Start mining, the possible combinations are {MinerInfo.lengthOfJob}";

                    // Build lookup set
                    MinerInfo.minerThreadInfo = "Reading address list size...";
                    int lookupAddresscount = 0;
                    using (var sr = new StreamReader(path))
                    {
                        while (sr.Peek() != -1)
                        {
                            sr.ReadLine();
                            lookupAddresscount++;
                        }
                    }

                    MinerInfo.minerThreadInfo = "Generating HashSet from address list...";
                    lookupSet = new HashSet<string>(lookupAddresscount);
                    using (var sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Length > 0) lookupSet.Add(line);
                        }
                    }

                    MinerInfo.minerThreadInfo = "Mining in parallel...";
                    StartWriter();

                    var charset = chars.ToCharArray();
                    int dop = Environment.ProcessorCount;

                    Parallel.ForEach(charset, _po, (prefix, state) =>
                    {
                        if (cancellationToken.IsCancellationRequested) return;

                        // Buffer candidates to reduce overhead; tune if needed
                        var buffer = new List<string>(4096);

                        foreach (var tail in GetAllMatches(charset, lengthOfString - 1))
                        {
                            if (cancellationToken.IsCancellationRequested) break;

                            var candidate = prefix + tail;
                            buffer.Add(candidate);

                            if (buffer.Count == 4096)
                            {
                                ProcessBatch(buffer);
                                buffer.Clear();
                            }

                            System.Threading.Interlocked.Increment(ref MinerInfo.currentlyProcessed);
                            System.Threading.Interlocked.Increment(ref MinerInfo.countOfTriedKeys);

                            if ((MinerInfo.currentlyProcessed % 50_000) == 0)
                                MinerInfo.minerThreadInfo = $"Last candidate: {candidate}";
                        }

                        if (buffer.Count > 0) ProcessBatch(buffer);
                    });
                }
                finally
                {
                    CompleteWriter();
                    stopwatch.Stop();

                    var elapsed = stopwatch.Elapsed.TotalSeconds;
                    var rate = elapsed > 0 ? (MinerInfo.countOfTriedKeys / elapsed) : 0.0;

                    MinerInfo.minerThreadResults =
                        $"{Environment.NewLine}Found keys: {foundKeyCount}, ckecked passwords: {MinerInfo.countOfTriedKeys} " +
                        $"{Environment.NewLine}Additional data: - Combinations: {MinerInfo.lengthOfJob}, elapsed time: {stopwatch.Elapsed}, keys/second: {rate}";

                    MinerInfo.minerStillRunning = false;
                    Dispose();
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void ProcessBatch(List<string> batch)
        {
            // Sequential inside each worker; workers run in parallel already
            foreach (var pw in batch)
            {
                var address = GetAddress(pw, compressed, countOfSha256Operation);
                if (address != "error" && lookupSet.Contains(address))
                {
                    System.Threading.Interlocked.Increment(ref foundKeyCount);
                    MinerInfo.minerThreadInfo = $"Found key!!!! Password: {pw}, address: {address}";

                    try { _foundLines.Add($"{pw} {address}", cancellationToken.Token); }
                    catch (OperationCanceledException) { return; }
                }
            }
        }

        private static IEnumerable<string> GetAllMatches(char[] charset, int length)
        {
            // length == 0 yields a single empty string (the tail of a 1-char prefix)
            if (length <= 0)
            {
                yield return string.Empty;
                yield break;
            }

            int[] indexes = new int[length];
            char[] current = new char[length];
            for (int i = 0; i < length; i++) current[i] = charset[0];

            do
            {
                yield return new string(current);
            }
            while (Increment(indexes, current, charset));
        }

        private static bool Increment(int[] indexes, char[] current, char[] chars)
        {
            int position = indexes.Length - 1;

            while (position >= 0)
            {
                indexes[position]++;
                if (indexes[position] < chars.Length)
                {
                    current[position] = chars[indexes[position]];
                    return true;
                }
                indexes[position] = 0;
                current[position] = chars[0];
                position--;
            }
            return false;
        }

        private long ComputeCombinations()
        {
            checked
            {
                int n = chars.Length;
                long total = 1;
                for (int i = 0; i < lengthOfString; i++)
                    total *= n;
                return total;
            }
        }

        private string GetAddress(string stringData, bool isCompressedKey = false, int sha256 = 1)
        {
            try
            {
                if (sha256 < 1) throw new InvalidOperationException();

                byte[] data = sha256 == 1
                    ? Casascius.Bitcoin.Util.ComputeSha256(stringData)
                    : GetHashSha256Deep(stringData, sha256);

                var privateKey = new Key(data, fCompressedIn: isCompressedKey);
                return privateKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString();
            }
            catch
            {
                return "error";
            }
        }

        private static byte[] GetHashSha256Deep(string input, int depth = 1)
        {
            string current = input;
            using var crypt = System.Security.Cryptography.SHA256.Create();
            var sb = new StringBuilder();

            for (int i = 0; i < depth - 1; i++)
            {
                byte[] hash = crypt.ComputeHash(Encoding.UTF8.GetBytes(current));
                foreach (byte b in hash) sb.Append(b.ToString("x2"));
                current = sb.ToString();
                sb.Clear();
            }

            return Casascius.Bitcoin.Util.ComputeSha256(current);
        }
    }
}
