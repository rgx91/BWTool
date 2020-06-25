using NBitcoin;
using System;
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
    class KeyRangeMiner : IDisposable
    {
        Stopwatch stopwatch = new Stopwatch();
        CancellationTokenSource cancellationToken;
        private bool hex;
        private bool increment;
        private int foundKeyCount = 0;
        string path;
        HashSet<string> lookupSet;
        string from;
        string until;
        bool compressed = false;
        Form form;
        RichTextBox textBox;
        public KeyRangeMiner(string pathOfLookupAddresses, bool compressed, bool hex, string from, string until, bool increment, Form form, RichTextBox richTextBox)
        {
            this.textBox = richTextBox;
            this.form = form;
            this.hex = hex;
            this.increment = increment;
            path = pathOfLookupAddresses;
            this.compressed = compressed;
            this.from = from;
            this.until = until;
            cancellationToken = new CancellationTokenSource();

        }
        public void StartIncrementalSearch()
        {
            Mine();
        }
        public void StopRandomStringMiner()
        {
            try
            {
                cancellationToken.Cancel();
            }
            catch (ObjectDisposedException)
            {


            }
        }

        private void Mine()
        {
            MinerInfo.minerStillRunning = true;


            var fromBiginteger = new Org.BouncyCastle.Math.BigInteger(Helper.ConvertBigintegerHexToDecimal(from, hex));
            var untilBiginteger = new Org.BouncyCastle.Math.BigInteger(Helper.ConvertBigintegerHexToDecimal(until, hex));
            var difference = fromBiginteger.Subtract(untilBiginteger).Abs();
            if (Helper.CompareNumbers(difference.ToString(), long.MaxValue.ToString()) > 0)
            {
                System.Windows.Forms.MessageBox.Show($"The length of job cant be more than {long.MaxValue}! Please use a smaller job length!");
                MinerInfo.minerStillRunning = false;
                return;
            }
            MinerInfo.lengthOfJob = Math.Abs(untilBiginteger.LongValue - fromBiginteger.LongValue);
            Task.Factory.StartNew(() =>
            {

                MinerInfo.minerThreadInfo = "Lookup size determine,generating hashset...";

                lookupSet = new HashSet<string>(10000);

                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() != -1)
                    {
                        lookupSet.Add(sr.ReadLine());
                    }
                }
                stopwatch.Start();
                MinerInfo.minerThreadInfo = "";
                MinerInfo.minerThreadInfo = "Lookup size determine, generating hashset done, mining...";

                while (fromBiginteger.CompareTo(untilBiginteger) < 0 && increment || fromBiginteger.CompareTo(untilBiginteger) > 0 && !increment)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    var key = KeyPair.Create(fromBiginteger, compressed);
                    if (lookupSet.Contains(key.AddressBase58))
                    {
                        MinerInfo.minerThreadInfo = $"Found key!!!! Number: {fromBiginteger.ToString()}, address: {key.AddressBase58}";
                        foundKeyCount++;
                        if (form.InvokeRequired)
                        {
                            form.Invoke((MethodInvoker)delegate { textBox.Text += $"{Environment.NewLine}Found key!!!! Number: {fromBiginteger.ToString()}, address: {key.AddressBase58}, privatekey: {key.PrivateKey}{Environment.NewLine}"; });



                        }
                        else
                        {

                        }
                        StreamWriter streamWriter = new StreamWriter("keys.txt", true);
                        string keyline = $"Found key!!!! Number: {fromBiginteger.ToString()}, address: {key.AddressBase58}, privatekey: {key.PrivateKey}";
                        streamWriter.WriteLine(keyline);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                    if (increment)
                    {

                        fromBiginteger = fromBiginteger.Add(new Org.BouncyCastle.Math.BigInteger("1"));
                    }
                    else
                    {
                        fromBiginteger = fromBiginteger.Add(new Org.BouncyCastle.Math.BigInteger("-1"));
                    }
                    MinerInfo.currentlyProcessed++;
                    MinerInfo.countOfTriedKeys++;
                    if (MinerInfo.currentlyProcessed % 1000 == 0)
                    {

                        MinerInfo.minerThreadInfo = $"Currently Processed number: {fromBiginteger.ToString()}";
                    }
                }
                //int lengthOfOneJob = 0;
                //if (MinerInfo.lengthOfJob > 10000)
                //{
                //    lengthOfOneJob = 10000;
                //}
                //else
                //{
                //    lengthOfOneJob = (int)MinerInfo.lengthOfJob;
                //}
                //StringBuilder stringBuilder = new StringBuilder(chars.Length);
                //for (int i = 0; i < chars.Length; i++)
                //{
                //    stringBuilder.Append(chars[0]);
                //}
                //List<string> sequenceList = new List<string>(lengthOfOneJob);
                //var list = GetAllMatches(chars.ToCharArray(), lengthOfString);
                //foreach (var item in list)
                //{
                //    if (cancellationToken.IsCancellationRequested)
                //    {
                //        break;
                //    }
                //    sequenceList.Add(item);
                //    if (sequenceList.Count == lengthOfOneJob)
                //    {
                //        LookupKeys(sequenceList);
                //        sequenceList.Clear();
                //    }
                //    MinerInfo.currentlyProcessed++;
                //    if (MinerInfo.currentlyProcessed >= MinerInfo.lengthOfJob && MinerInfo.lengthOfJob > 10000)
                //    {
                //        LookupKeys(sequenceList);
                //        break;
                //    }
                //}
                //int count = list.Count();

                MinerInfo.minerThreadResults = $"{Environment.NewLine}Found keys: {foundKeyCount}, ckecked numbers: {MinerInfo.countOfTriedKeys} {Environment.NewLine}Additional data: - Combinations: {MinerInfo.lengthOfJob}, elapsed time: {stopwatch.Elapsed}, keys/second: {MinerInfo.countOfTriedKeys / stopwatch.Elapsed.TotalSeconds}";
                Dispose();
                MinerInfo.minerStillRunning = false;
            }, TaskCreationOptions.LongRunning);



        }


        public void Dispose()
        {
            //Not used right now
        }
    }
}
