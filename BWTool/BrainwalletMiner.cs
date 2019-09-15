using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NBitcoin;

namespace BWTool
{
    class BrainwalletMiner : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        StreamReader streamReaderOfPassphraseFile;
        StreamReader streamReaderOfAddressesFile;
        private int foundKeyCount = 0;
        Filter<string> lookupBloom;
        string pathOfPassphraseFile;
        string pathOfLookupAddresses;
        char separator;
        bool compressed = false;
        int countOfSha256Operation;
        CancellationTokenSource cancellationToken;
        Stopwatch stopwatch = new Stopwatch();
 


        public BrainwalletMiner(string pathOfPassphraseFile, string pathOfLookupAddresses, bool compressed, char separator, int countOfSha256Operation)
        {
            streamReaderOfPassphraseFile = new StreamReader(pathOfPassphraseFile);
            streamReaderOfAddressesFile = new StreamReader(pathOfLookupAddresses);
            this.compressed = compressed;
            cancellationToken = new CancellationTokenSource();
            this.separator = separator;
            this.pathOfLookupAddresses = pathOfLookupAddresses;
            this.pathOfPassphraseFile = pathOfPassphraseFile;
            this.countOfSha256Operation = countOfSha256Operation;
  
        }

        public void Dispose()
        {
            streamReaderOfPassphraseFile.Dispose();
            streamReaderOfAddressesFile.Dispose();
            cancellationToken.Dispose();
            _lock.Dispose();
            
        }
        public void Start()
        {

            ProcessPasswordList();

        }
        public void Stop()
        {
            try
            {
                cancellationToken.Cancel();
            }
            catch (ObjectDisposedException)
            {

                
            }


        }
        private void ProcessPasswordList()
        {
            MinerInfo.minerStillRunning = true;
            stopwatch.Start();
            Task.Factory.StartNew(() =>
            {
                MinerInfo.minerThreadInfo = ""; 
                MinerInfo.minerThreadInfo= "Reading file length...";
                using (StreamReader streamReader = new StreamReader(pathOfPassphraseFile))
                {
                    while (streamReader.Peek() != -1 && !cancellationToken.IsCancellationRequested)
                    {
                        streamReader.ReadLine();
                        MinerInfo.lengthOfJob++;
                    }
                }
                int lookupAddresscount = 0;
                using (StreamReader sr = new StreamReader(pathOfLookupAddresses))
                {
                    while (sr.Peek()!=-1)
                    {
                        sr.ReadLine();
                        lookupAddresscount++;
                    }

                }
                MinerInfo.minerThreadInfo = ""; 
                MinerInfo.minerThreadInfo = "Reading file done,generating bloomfilter...";
                lookupBloom = new Filter<string>(lookupAddresscount);
                using (StreamReader sr = new StreamReader(pathOfLookupAddresses))
                {
                    lookupBloom.Add(sr.ReadLine());
                }
              //  lookupAdresses = new HashSet<string>(File.ReadAllLines(pathOfLookupAddresses));
                MinerInfo.minerThreadInfo = ""; 
                MinerInfo.minerThreadInfo = "Generating bloomfilter done, mining...";
                using (StreamReader streamReader = new StreamReader(pathOfPassphraseFile))
                {

                    List<string> lines = new List<string>((int)MinerInfo.lengthOfJob / 100);
                    while (streamReader.Peek() != -1 && !cancellationToken.IsCancellationRequested)
                    {
                        lines.Add(streamReader.ReadLine());
                        MinerInfo.currentlyProcessed++;
                        if (MinerInfo.currentlyProcessed % (MinerInfo.lengthOfJob / 100) == 0)
                        {
                            ParseLines(lines);
                            lines.Clear();

                        }
                        if (MinerInfo.currentlyProcessed == MinerInfo.lengthOfJob)
                        {
                            ParseLines(lines);
                        }
                    }
                    stopwatch.Stop();
                    MinerInfo.minerThreadResults= $"{Environment.NewLine}Found keys: {foundKeyCount}, ckecked passwords: {MinerInfo.countOfTriedKeys} {Environment.NewLine}Additional data: - password file length: {MinerInfo.lengthOfJob} line, elapsed time: {stopwatch.Elapsed}, keys/second: {MinerInfo.countOfTriedKeys / stopwatch.Elapsed.TotalSeconds}";
                    Dispose();
                    MinerInfo.minerStillRunning = false;
                }





            }
            , TaskCreationOptions.LongRunning);
        }
        private void ParseLines(List<string> textLines)
        {
            //Splitting here on user selected separator
            if (separator != '\r')
            {
                List<string> splittedList = new List<string>(1000);
                StringBuilder localStringBuilder = new StringBuilder(1000);
                for (int i = 0; i < textLines.Count; i++)
                {
                    //if there is a separator, the preceding part of line splitted and added to list, the last one will be part of the next line, appended to stringbuilder
                    if (textLines[i].Contains(separator))
                    {
                        var splittedString = textLines[i].Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < splittedString.Length - 1; j++)
                        {
                            localStringBuilder.Append(splittedString[j]);
                            splittedList.Add(localStringBuilder.ToString());
                            localStringBuilder.Clear(); ;
                        }
                        if (splittedString.Length == 1)
                        {
                            localStringBuilder.Append(splittedString[0]);
                            splittedList.Add(localStringBuilder.ToString());
                            localStringBuilder.Clear();
                        }
                        else
                        {
                            localStringBuilder.Append(splittedString.Last() + " ");
                        }

                    }
                    else
                    {
                        //Appending while there is a separator, the space is instead of new line
                        localStringBuilder.Append(textLines[i] + " ");
                    }
                }
                LookupKeys(splittedList);
            }
            else
            {
                LookupKeys(textLines);;
            }
        }
        private void LookupKeys(List<string> lookupAdressesList)
        {

                Parallel.ForEach(lookupAdressesList, (line, state) =>
                {
                    Interlocked.Increment(ref MinerInfo.countOfTriedKeys);
                    if (ThreadSafeContains(GetAddress(line, compressed, countOfSha256Operation)))
                    {
                        MinerInfo.minerThreadInfo = $"Found key!!!! Password: {line}, address: {GetAddress(line, compressed, countOfSha256Operation)}";
                        Interlocked.Increment(ref foundKeyCount);

                        StreamWriter streamWriter = new StreamWriter("keys.txt", true);
                        lock (streamWriter)
                        {
                            string key = line + " " + GetAddress(line, compressed, countOfSha256Operation);
                            streamWriter.WriteLine(key);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    if (cancellationToken.IsCancellationRequested)
                    {
                        state.Break();
                    }
                });

            

        }
        private string GetAddress(string stringData, bool isCompressedKey = false, int sha256 = 1)
        {
            try
            {
                byte[] data;
                if (sha256 < 1)
                {
                    throw new InvalidOperationException();
                }
                if (sha256 == 1)
                {
                    data = Casascius.Bitcoin.Util.ComputeSha256(stringData);
                }
                else
                {
                    data = getHashSha256(stringData, sha256);
                }

                var privateKey = isCompressedKey ? new Key(data, fCompressedIn: true) : new Key(data, fCompressedIn: false);
                return privateKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString(); 
               // var bitcoinPrivateKey = privateKey.GetWif(Network.Main);
                //var bitcoinAddress = bitcoinPrivateKey.GetAddress(ScriptPubKeyType.Legacy).ToString();
                //return bitcoinAddress;
            }
            catch (Exception)
            {
                return ("error");
            }
        }
        private static byte[] getHashSha256(string randomString, int deepOfSha = 1)
        {
            string returnedHash = randomString;
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder localHashBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < deepOfSha - 1; i++)// Casascius.Bitcoin.Util.ComputeSha256 computes one level of sha256 hash
            {
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(returnedHash), 0, Encoding.UTF8.GetByteCount(returnedHash));
                foreach (byte theByte in crypto)
                {
                    localHashBuilder.Append(theByte.ToString("x2"));
                }
                returnedHash = localHashBuilder.ToString();
                localHashBuilder.Clear();
            }

            return Casascius.Bitcoin.Util.ComputeSha256(returnedHash);
        }
        private bool ThreadSafeContains(string item)
        {
            _lock.EnterReadLock();
            try
            {
                return lookupBloom.Contains(item);
            }
            finally
            {
                if (_lock.IsReadLockHeld) _lock.ExitReadLock();
            }
        }

    }
}
