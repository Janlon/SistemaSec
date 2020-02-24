using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sec
{

    public class SuperKey: IDisposable
    {
        private string Key { get; set; }
        public static string Create()
        {
            string ret = "";
            using(SuperKey k=new SuperKey())
                ret = k.Key;
            return ret;
        }

        #region Instância
        private bool disposedValue;
        protected virtual void Dispose(bool disposing){if (!disposedValue){if (disposing){CleanUp();}disposedValue = true;}}
        private void CleanUp() { }
        ~SuperKey() { Dispose(false); }
        internal SuperKey()
        {
            disposedValue = false;
            for (int i = 0; i < 5; i++)
                Key = string.Format("{0}{1}", Key, Guid
                    .NewGuid()
                    .ToString("N")
                    .JustLettersAndDigits())
                    .Trim()
                    .ToUpper();
            Key = Key
                    .Substring(64, 32)
                    .Trim();
        }
        public void Dispose(){Dispose(true);GC.SuppressFinalize(this);}
        #endregion
    }

    public static class StringExtensions
    {
        public static string JustSymbols(this string value)
        {
            string ret = "";
            try
            {
                foreach (char c in value)
                    if (char.IsSymbol(c))
                        ret += c;
            }
            catch { ret = value; }
            return ret;
        }

        public static string JustLettersAndDigits(this string value)
        {
            string ret = "";
            try
            {
                foreach (char c in value)
                    if (char.IsLetterOrDigit(c))
                        ret += c;
            }
            catch { ret = value; }
            return ret;
        }

        public static string JustLetters(this string value)
        {
            string ret = "";
            try
            {
                foreach (char c in value)
                    if (char.IsLetter(c))
                        ret += c;
            }
            catch { ret = value; }
            return ret;
        }

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
