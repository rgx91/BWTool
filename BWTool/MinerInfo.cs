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
        public static int passwordFileLineLength = 0;
        public static int currentlyProcessedLine = 0;
        public static string minerThreadInfo = "";
        public static string minerThreadResults = "";
        public static bool minerStillRunning = false;
        public static void defaultingValues()
        {
            countOfTriedKeys = 0;
            passwordFileLineLength = 0;
            currentlyProcessedLine = 0;
            minerThreadInfo = "";
            minerThreadResults = "";
            minerStillRunning = false;
        }
    }
}
