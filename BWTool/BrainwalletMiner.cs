using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NBitcoin;

namespace BWTool
{
    /// <summary>
    /// Parallelized brainwallet miner.
    /// - Reads the lookup address list into a HashSet (read-only afterward).
    /// - Streams the passphrase file in batches.
    /// - Splits per the configured separator.
    /// - Processes each batch with Parallel.ForEach.
    /// - Uses a single writer thread (BlockingCollection) to append matches to keys.txt.
    /// - Uses Interlocked for counters.
    /// </summary>
    class BrainwalletMiner : IDisposable
    {
        // config / inputs
        private readonly string pathOfPassphraseFile;
        private readonly string pathOfLookupAddresses;
        private readonly char separator;
        private readonly bool compressed;
        private readonly int countOfSha256Operation;

        // state
        private StreamReader streamReaderOfPassphraseFile;
        private StreamReader streamReaderOfAddressesFile;
        private HashSet<string> lookupSet;
        private readonly CancellationTokenSource cancellationToken;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly ParallelOptions _po;
        private volatile int _foundKeyCount;

        // single writer for output
        private readonly BlockingCollection<string> _foundLines = new BlockingCollection<string>(10_000);
        private Task _writerTask;

        public BrainwalletMiner(string pathOfPassphraseFile,
                                string pathOfLookupAddresses,
                                bool compressed,
                                char separator,
                                int countOfSha256Operation)
        {
            this.pathOfPassphraseFile = pathOfPassphraseFile ?? throw new ArgumentNullException(nameof(pathOfPassphraseFile));
            this.pathOfLookupAddresses = pathOfLookupAddresses ?? throw new ArgumentNullException(nameof(pathOfLookupAddresses));
            this.compressed = compressed;
            this.separator = separator;
            this.countOfSha256Operation = countOfSha256Operation;

            streamReaderOfPassphraseFile = new StreamReader(pathOfPassphraseFile);
            streamReaderOfAddressesFile = new StreamReader(pathOfLookupAddresses);

            cancellationToken = new CancellationTokenSource();

            _po = new ParallelOptions
            {
                // Don't pass the token here to avoid OperationCanceledException bubbling from Parallel.ForEach.
                // We will check token cooperatively inside the loop.
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
        }

        public void Start()
        {
            ProcessPasswordList();
        }

        public void Stop()
        {
            try { cancellationToken.Cancel(); } catch (ObjectDisposedException) { /* no-op */ }
        }

        public void Dispose()
        {
            try { streamReaderOfPassphraseFile?.Dispose(); } catch { }
            try { streamReaderOfAddressesFile?.Dispose(); } catch { }
            try { cancellationToken?.Dispose(); } catch { }
            try { _foundLines?.Dispose(); } catch { }
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
            catch (OperationCanceledException) { /* graceful stop */ }
            catch (AggregateException ae) when (ae.InnerExceptions.All(e => e is OperationCanceledException))
            {
                /* graceful stop */
            }
        }

        private void ProcessPasswordList()
        {
            MinerInfo.minerStillRunning = true;
            stopwatch.Start();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    MinerInfo.minerThreadInfo = "Reading file length...";
                    // Count lines in passphrase file
                    using (var reader = new StreamReader(pathOfPassphraseFile))
                    {
                        long lines = 0;
                        while (reader.Peek() != -1 && !cancellationToken.IsCancellationRequested)
                        {
                            reader.ReadLine();
                            lines++;
                        }
                        MinerInfo.lengthOfJob = lines;
                    }

                    // Build lookup set
                    MinerInfo.minerThreadInfo = "Reading address list...";
                    int lookupAddresscount = 0;
                    using (var sr = new StreamReader(pathOfLookupAddresses))
                    {
                        while (sr.Peek() != -1) { sr.ReadLine(); lookupAddresscount++; }
                    }

                    MinerInfo.minerThreadInfo = "Generating HashSet...";
                    lookupSet = new HashSet<string>(lookupAddresscount);
                    using (var sr = new StreamReader(pathOfLookupAddresses))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Length > 0) lookupSet.Add(line);
                        }
                    }

                    MinerInfo.minerThreadInfo = "Mining...";
                    StartWriter();

                    var batchSizeDiv = Math.Max(1L, MinerInfo.lengthOfJob / 100L); // avoid div by zero on small files
                    var batch = new List<string>(checked((int)Math.Min(int.MaxValue, batchSizeDiv)));

                    using (var reader = new StreamReader(pathOfPassphraseFile))
                    {
                        string line;
                        while (!cancellationToken.IsCancellationRequested && (line = reader.ReadLine()) != null)
                        {
                            batch.Add(line);
                            Interlocked.Increment(ref MinerInfo.currentlyProcessed);

                            if (MinerInfo.currentlyProcessed % batchSizeDiv == 0)
                            {
                                ParseLines(batch);
                                batch.Clear();
                            }
                        }

                        if (batch.Count > 0) ParseLines(batch);
                    }
                }
                finally
                {
                    CompleteWriter();
                    stopwatch.Stop();

                    var elapsed = stopwatch.Elapsed.TotalSeconds;
                    var rate = elapsed > 0 ? (MinerInfo.countOfTriedKeys / elapsed) : 0.0;

                    MinerInfo.minerThreadResults =
                        $"{Environment.NewLine}Found keys: {_foundKeyCount}, ckecked passwords: {MinerInfo.countOfTriedKeys} " +
                        $"{Environment.NewLine}Additional data: - password file length: {MinerInfo.lengthOfJob} line, " +
                        $"elapsed time: {stopwatch.Elapsed}, keys/second: {rate}";

                    MinerInfo.minerStillRunning = false;
                    Dispose();
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void ParseLines(List<string> textLines)
        {
            if (textLines == null || textLines.Count == 0) return;

            // If separator is not Enter, we split across lines and carry tails
            if (separator != '\r')
            {
                var splittedList = new List<string>(textLines.Count * 2);
                var local = new StringBuilder(1024);

                for (int i = 0; i < textLines.Count; i++)
                {
                    var line = textLines[i];
                    if (line.IndexOf(separator) >= 0)
                    {
                        var parts = line.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                        // all but the last part complete a token
                        for (int j = 0; j < parts.Length - 1; j++)
                        {
                            local.Append(parts[j]);
                            var token = local.ToString();
                            if (token.Length > 0) splittedList.Add(token);
                            local.Clear();
                        }

                        if (parts.Length == 1)
                        {
                            local.Append(parts[0]);
                            var tokenSingle = local.ToString();
                            if (tokenSingle.Length > 0) splittedList.Add(tokenSingle);
                            local.Clear();
                        }
                        else
                        {
                            // keep last part as carryover (with space instead of newline)
                            local.Append(parts.Last());
                            local.Append(' ');
                        }
                    }
                    else
                    {
                        // no separator in this line; append and keep carrying
                        local.Append(line);
                        local.Append(' ');
                    }
                }

                // anything in the builder at the very end is an incomplete token; drop it

                LookupKeysParallel(splittedList);
            }
            else
            {
                // Enter-separated; each line is a token as-is
                LookupKeysParallel(textLines);
            }
        }

        private void LookupKeysParallel(List<string> candidates)
        {
            if (candidates == null || candidates.Count == 0) return;

            try
            {
                Parallel.ForEach(candidates, _po, pw =>
                {
                    if (cancellationToken.IsCancellationRequested) return;

                    Interlocked.Increment(ref MinerInfo.countOfTriedKeys);

                    var address = GetAddress(pw, compressed, countOfSha256Operation);
                    if (address != "error" && lookupSet.Contains(address))
                    {
                        Interlocked.Increment(ref _foundKeyCount);
                        MinerInfo.minerThreadInfo = $"Found key!!!! Password: {pw}, address: {address}";

                        // enqueue for the single writer
                        try { _foundLines.Add($"{pw} {address}", cancellationToken.Token); }
                        catch (OperationCanceledException) { /* shutting down */ }
                    }
                });
            }
            catch (OperationCanceledException)
            {
                // cooperative cancel
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
