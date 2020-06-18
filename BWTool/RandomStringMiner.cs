using NBitcoin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BWTool
{
    class RandomStringMiner : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        Stopwatch stopwatch = new Stopwatch();
        int lengthOfString = 0;
        Random random = new Random();
        CancellationTokenSource cancellationToken;
        private int foundKeyCount = 0;
        string chars = "";
        string path;
        HashSet<string> lookupSet;
        bool compressed = false;
        int countOfSha256Operation;
        public RandomStringMiner(int lengthOfString, string pathOfLookupAddresses, bool compressed, int countOfSha256Operation, string charWorkingSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            this.lengthOfString = lengthOfString;
            this.chars = string.Join("", charWorkingSet.Distinct());
            path = pathOfLookupAddresses;
            this.compressed = compressed;
            this.countOfSha256Operation = countOfSha256Operation;
            cancellationToken = new CancellationTokenSource();

        }
        string RandomString(int length)
        {

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s.ToLower()[random.Next(s.Length)]).ToArray());
        }
        public void StartRandomStringMiner()
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
            stopwatch.Start();
            Task.Factory.StartNew(() =>
            {
                MinerInfo.minerThreadInfo = "";
                MinerInfo.lengthOfJob = ComputeCombinations();
                MinerInfo.minerThreadInfo = $"Start mining, the possible combinations are {MinerInfo.lengthOfJob}";
                int lookupAddresscount = 0;

                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() != -1)
                    {
                        sr.ReadLine();
                        lookupAddresscount++;
                    }

                }
                MinerInfo.minerThreadInfo = "";
                MinerInfo.minerThreadInfo = "Reading file size done,generating hashset...";
                lookupSet = new HashSet<string>(lookupAddresscount);
                using (StreamReader sr = new StreamReader(path))
                {
                    lookupSet.Add(sr.ReadLine());
                }
                MinerInfo.minerThreadInfo = ""; ;
                MinerInfo.minerThreadInfo = "Generating hashset done, mining...";
                int lengthOfOneJob = 0;
                if (MinerInfo.lengthOfJob > 10000)
                {
                    lengthOfOneJob = 10000;
                }
                else
                {
                    lengthOfOneJob = (int)MinerInfo.lengthOfJob;
                }
                StringBuilder stringBuilder = new StringBuilder(chars.Length);
                for (int i = 0; i < chars.Length; i++)
                {
                    stringBuilder.Append(chars[0]);
                }
                List<string> sequenceList = new List<string>(lengthOfOneJob);
                var list = GetAllMatches(chars.ToCharArray(), lengthOfString);
                foreach (var item in list)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    sequenceList.Add(item);
                    if (sequenceList.Count == lengthOfOneJob)
                    {
                        LookupKeys(sequenceList);
                        sequenceList.Clear();
                    }
                    MinerInfo.currentlyProcessed++;
                    if (MinerInfo.currentlyProcessed >= MinerInfo.lengthOfJob && MinerInfo.lengthOfJob > 10000)
                    {
                        LookupKeys(sequenceList);
                        break;
                    }
                }
                int count = list.Count();

                MinerInfo.minerThreadResults = $"{Environment.NewLine}Found keys: {foundKeyCount}, ckecked passwords: {MinerInfo.countOfTriedKeys} {Environment.NewLine}Additional data: - Combinations: {MinerInfo.lengthOfJob}, elapsed time: {stopwatch.Elapsed}, keys/second: {MinerInfo.countOfTriedKeys / stopwatch.Elapsed.TotalSeconds}";
                Dispose();
                MinerInfo.minerStillRunning = false;
            }, TaskCreationOptions.LongRunning);



        }
        private static IEnumerable<string> GetAllMatches(char[] chars, int length)
        {
            int[] indexes = new int[length];
            char[] current = new char[length];
            for (int i = 0; i < length; i++)
            {
                current[i] = chars[0];
            }
            do
            {
                yield return new string(current);
            }
            while (Increment(indexes, current, chars));
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

                int lengthOfCharSet = chars.Length;
                long returnValue = lengthOfCharSet;
                for (int i = 1; i < lengthOfString; i++)
                {
                    returnValue *= (long)lengthOfCharSet;
                }
                return returnValue;
            }

        }
        private void LookupKeys(List<string> lookupAdressesList)
        {
            for (int i = 0; i < lookupAdressesList.Count; i++)
            {

                MinerInfo.countOfTriedKeys++;
                if (lookupSet.Contains(GetAddress(lookupAdressesList[i], compressed, countOfSha256Operation)))
                {
                    MinerInfo.minerThreadInfo = $"Found key!!!! Password: {lookupAdressesList[i]}, address: {GetAddress(lookupAdressesList[i], compressed, countOfSha256Operation)}";
                    foundKeyCount++;

                    StreamWriter streamWriter = new StreamWriter("keys.txt", true);
                    string key = lookupAdressesList[i] + " " + GetAddress(lookupAdressesList[i], compressed, countOfSha256Operation);
                    streamWriter.WriteLine(key);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

            }
            MinerInfo.minerThreadInfo = $"Currently Processed word: {lookupAdressesList.Last()}";




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

        public void Dispose()
        {
            _lock.Dispose();
        }
    }
}
