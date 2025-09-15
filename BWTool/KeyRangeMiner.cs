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
using System.Numerics;
using System.Globalization;
using Casascius.Bitcoin;
using System.Windows.Forms;

namespace BWTool
{
    /// <summary>
    /// Parallelized numeric private-key range miner.
    /// - Loads lookup addresses into a HashSet once.
    /// - Splits the numeric range across CPU cores using a striding pattern.
    /// - Uses Interlocked for thread-safe counters.
    /// - Writes matches through a single writer thread (BlockingCollection) to avoid contention.
    /// </summary>
    class KeyRangeMiner : IDisposable
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly CancellationTokenSource cancellationToken;

        private readonly bool hex;
        private readonly bool increment;
        private volatile int foundKeyCount = 0;

        private readonly string path;
        private HashSet<string> lookupSet;

        private readonly string from;
        private readonly string until;
        private readonly bool compressed;

        // UI
        private readonly Form form;
        private readonly RichTextBox textBox;

        // Output writer (single consumer)
        private readonly BlockingCollection<string> _foundLines = new BlockingCollection<string>(10_000);
        private Task _writerTask;

        public KeyRangeMiner(string pathOfLookupAddresses, bool compressed, bool hex, string from, string until, bool increment, Form form, RichTextBox richTextBox)
        {
            this.textBox = richTextBox;
            this.form = form;
            this.hex = hex;
            this.increment = increment;
            this.path = pathOfLookupAddresses ?? throw new ArgumentNullException(nameof(pathOfLookupAddresses));
            this.compressed = compressed;
            this.from = from ?? throw new ArgumentNullException(nameof(from));
            this.until = until ?? throw new ArgumentNullException(nameof(until));
            this.cancellationToken = new CancellationTokenSource();
        }

        public void StartIncrementalSearch()
        {
            Mine();
        }

        public void StopRandomStringMiner()
        {
            try { cancellationToken.Cancel(); } catch (ObjectDisposedException) { /* ignore */ }
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

            var fromBiginteger = new Org.BouncyCastle.Math.BigInteger(Helper.ConvertBigintegerHexToDecimal(from, hex));
            var untilBiginteger = new Org.BouncyCastle.Math.BigInteger(Helper.ConvertBigintegerHexToDecimal(until, hex));
            var difference = fromBiginteger.Subtract(untilBiginteger).Abs();

            if (Helper.CompareNumbers(difference.ToString(), long.MaxValue.ToString()) > 0)
            {
                MessageBox.Show($"The length of job cant be more than {long.MaxValue}! Please use a smaller job length!");
                MinerInfo.minerStillRunning = false;
                return;
            }

            MinerInfo.lengthOfJob = Math.Abs(untilBiginteger.LongValue - fromBiginteger.LongValue);

            Task.Factory.StartNew(() =>
            {
                try
                {
                    MinerInfo.minerThreadInfo = "Lookup size determine, generating hashset...";

                    lookupSet = new HashSet<string>(capacity: 10000);
                    using (var sr = new StreamReader(path))
                    {
                        string? line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Length > 0)
                                lookupSet.Add(line);
                        }
                    }

                    stopwatch.Start();
                    MinerInfo.minerThreadInfo = "HashSet ready, mining in parallel...";

                    StartWriter();

                    int dop = Environment.ProcessorCount;
                    var stride = new Org.BouncyCastle.Math.BigInteger(dop.ToString());

                    Parallel.For(0, dop, new ParallelOptions { CancellationToken = cancellationToken.Token, MaxDegreeOfParallelism = dop }, workerId =>
                    {
                        // establish each worker's starting key and step direction
                        Org.BouncyCastle.Math.BigInteger cur;
                        Org.BouncyCastle.Math.BigInteger step;

                        if (increment)
                        {
                            cur = fromBiginteger.Add(new Org.BouncyCastle.Math.BigInteger(workerId.ToString()));
                            step = stride; // +dop
                        }
                        else
                        {
                            cur = fromBiginteger.Subtract(new Org.BouncyCastle.Math.BigInteger(workerId.ToString()));
                            step = stride.Negate(); // -dop
                        }

                        // bounds check lambda
                        bool InBounds(Org.BouncyCastle.Math.BigInteger x) =>
                            increment ? (x.CompareTo(untilBiginteger) < 0)
                                      : (x.CompareTo(untilBiginteger) > 0);

                        while (!cancellationToken.IsCancellationRequested && InBounds(cur))
                        {
                            // Check this key
                            CheckAndRecord(cur);

                            // advance
                            cur = cur.Add(step);

                            // progress
                            Interlocked.Increment(ref MinerInfo.countOfTriedKeys);
                            Interlocked.Increment(ref MinerInfo.currentlyProcessed);

                            if ((MinerInfo.currentlyProcessed % 1000) == 0)
                                MinerInfo.minerThreadInfo = $"Currently processed near: {cur}";
                        }
                    });
                }
                finally
                {
                    CompleteWriter();
                    stopwatch.Stop();

                    var elapsed = stopwatch.Elapsed.TotalSeconds;
                    var rate = elapsed > 0 ? (MinerInfo.countOfTriedKeys / elapsed) : 0.0;

                    MinerInfo.minerThreadResults =
                        $"{Environment.NewLine}Found keys: {foundKeyCount}, ckecked numbers: {MinerInfo.countOfTriedKeys} " +
                        $"{Environment.NewLine}Additional data: - Combinations: {MinerInfo.lengthOfJob}, elapsed time: {stopwatch.Elapsed}, keys/second: {rate}";

                    Dispose();
                    MinerInfo.minerStillRunning = false;
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void CheckAndRecord(Org.BouncyCastle.Math.BigInteger privNum)
        {
            try
            {
                var key = KeyPair.Create(privNum, compressed);
                var addr = key.AddressBase58;

                if (lookupSet.Contains(addr))
                {
                    Interlocked.Increment(ref foundKeyCount);

                    string line = $"Found key!!!! Number: {privNum}, address: {addr}, privatekey: {key.PrivateKey}";
                    MinerInfo.minerThreadInfo = line;

                    // UI append (non-blocking)
                    if (form != null && textBox != null)
                    {
                        try
                        {
                            form.BeginInvoke((MethodInvoker)(() =>
                            {
                                textBox.AppendText($"{Environment.NewLine}{line}{Environment.NewLine}");
                            }));
                        }
                        catch { /* ignore UI race on shutdown */ }
                    }

                    // queue for file writer
                    try { _foundLines.Add(line, cancellationToken.Token); }
                    catch (OperationCanceledException) { /* shutting down */ }
                }
            }
            catch
            {
                // ignore invalid numbers (should be filtered by UI, but just in case)
            }
        }

        public void Dispose()
        {
            // Nothing to dispose explicitly beyond what we already handle.
        }
    }
}
