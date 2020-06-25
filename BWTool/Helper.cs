using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BWTool
{
    static class Helper
    {
        public static bool OnlyHexInString(string test)
        {
            // For C-style hex notation (0xFF) you can use @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
            return System.Text.RegularExpressions.Regex.IsMatch(test, @"\A\b[0-9a-fA-F]+\b\Z");
        }
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public static string ConvertBigintegerHexToDecimal(string biginteger,bool hex)
        {
            string bouncyCastleCompatibleBiginteger = "";
            if (hex)
            {

            var number = BigInteger.Parse("0" + biginteger, NumberStyles.HexNumber);
            bouncyCastleCompatibleBiginteger = number.ToString();
            }
            else
            {
                bouncyCastleCompatibleBiginteger = biginteger;
            }
            return bouncyCastleCompatibleBiginteger.StartsWith("0")
                ? bouncyCastleCompatibleBiginteger.TrimStart('0')
                : bouncyCastleCompatibleBiginteger;
        }
        public static int CompareNumbers(string x, string y)
        {
            if (x.Length > y.Length) y = y.PadLeft(x.Length, '0');
            else if (y.Length > x.Length) x = x.PadLeft(y.Length, '0');

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] < y[i]) return -1;
                if (x[i] > y[i]) return 1;
            }
            return 0;
        }
        public static string ConvertBigintegerTextBetweenHexAndDecimal(string biginteger, bool hex)
        {
            try
            {
                if (biginteger.Length > 0)
                {
                    if (hex)
                    {
                        var number = BigInteger.Parse(biginteger, NumberStyles.Integer);
                        return number.ToString("X");

                    }
                    else
                    {
                        //FromTextbox.Text.ToCharArray().Any(c => !"0123456789abcdefABCDEF".Contains(c))
                        if (biginteger.StartsWith("0"))
                        {
                            var number = BigInteger.Parse(biginteger, NumberStyles.HexNumber);
                            return number.ToString();
                        }
                        else
                        {
                            var number = BigInteger.Parse("0" + biginteger, NumberStyles.HexNumber);
                            return number.ToString();
                        }



                    }


                }
                return "";
            }
            catch (FormatException ex)
            {

                return "";
            }
        }
    }
}
