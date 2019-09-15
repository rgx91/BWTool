using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWTool
{
    static class MinerInfo
    {
        public static int countOfTriedKeys = 0;
        public static long lengthOfJob = 0;
        public static long currentlyProcessed = 0;
        public static string minerThreadInfo = "";
        public static string minerThreadResults = "";
        public static bool minerStillRunning = false;
        public static void defaultingValues()
        {
            countOfTriedKeys = 0;
            lengthOfJob = 0;
            currentlyProcessed = 0;
            minerThreadInfo = "";
            minerThreadResults = "";
            minerStillRunning = false;
        }
    }
}
