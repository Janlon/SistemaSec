using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sec
{
    public static class StringExtensions
    {
        public static string JustNumbers(this string value)
        {
            string ret = "";
            try
            {
                foreach (char c in value)
                    if (char.IsDigit(c))
                        ret += c;
            }
            catch { ret = value; }
            return ret;
        }
    }
}
