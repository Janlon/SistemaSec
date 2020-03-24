namespace Generics.Extensoes
{
    using Generics.Helpers;
    using System;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extensões para criptografia de textos.
    /// </summary>
    public static class StringExtensions
    {

        public static string RemoveChars(this string valor, params char[] caract)
        {
            if (String.IsNullOrWhiteSpace(valor))
                return String.Empty;
            var sb = new StringBuilder();
            foreach (var c in valor)
            {
                bool adicionar = true;
                for (var j = 0; j < caract.Length; j++)
                {
                    if (caract[j] == c)
                    {
                        adicionar = false;
                        break;
                    }
                }
                if (adicionar)
                    sb.Append(c);
            }
            return sb.ToString();
        }

        public static bool IsCNPJ(this string value)
        {
            value = value.JustNumbers();
            Cnpj cnpj = new Cnpj(value);
            if (cnpj.CalculaNumeroDeDigitos() != 14)
                return false;
            if (cnpj.VerficarSeTodosOsDigitosSaoIdenticos())
                return false;
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var soma1 = 0;
            var soma2 = 0;
            for (var i = 0; i < 12; i++)
            {
                var d = cnpj.ObterDigito(i);
                soma1 += d * multiplicador1[i];
                soma2 += d * multiplicador2[i];
            }
            var resto = (soma1 % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = (11 - resto);
            var dv1 = resto;
            soma2 += resto * multiplicador2[12];
            resto = (soma2 % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = (11 - resto);
            var dv2 = resto;
            return cnpj.ObterDigito(12) == dv1 && cnpj.ObterDigito(13) == dv2;
        }
        public static bool IsCPF(this string value) { return new Cpf(value).EhValido; }

        public static bool IsIPAddress(this string value)
        {
            bool ret = false;
            try
            {
                IPAddress saida = null;
                ret = IPAddress.TryParse(value, out saida);
            }
            catch { ret = false; }
            return ret;
        }

        public static bool IsHtml(this string value)
        {
            bool ret = false;
            Regex tagRegex = new Regex(@"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
            ret = tagRegex.IsMatch("" + value + "");
            if (!ret)
            {
                tagRegex = new Regex(@"<[^>]+>");
                ret = tagRegex.IsMatch("" + value + "");
            }
            return ret;
        }

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

        /// <summary>
        /// Retorna a parte deste texto composta por letras apenas.
        /// </summary>
        /// <param name="value">Este texto.</param>
        /// <returns>Resultado.</returns>
        public static string SuperString(int cicles = 10)
        {
            string ret = "";
            for (int i = 0; i < cicles; i++)
                ret += string.Format("{0}", Guid.NewGuid()).JustLetters();
            return ret.Trim().JustLetters();
        }
        /// <summary>
        /// Criptografa este texto utilizando a chave indicada.
        /// </summary>
        /// <param name="value">Este texto.</param>
        /// <param name="key">Chave criptográfica.</param>
        /// <returns>Resultado.</returns>
        public static string Encrypt(this string value, string key)
        {
            string ret = "";
            using (Crypto c = new Crypto(value, key, Crypto.Operation.Encrypt))
                ret = c.Result;
            return ret;
        }
        /// <summary>
        /// Decriptografa este texto utilizando a chave indicada.
        /// </summary>
        /// <param name="value">Este texto.</param>
        /// <param name="key">Chave criptográfica.</param>
        /// <returns>Resultado.</returns>
        public static string Decrypt(this string value, string key)
        {
            string ret = "";
            using (Crypto c = new Crypto(value, key, Crypto.Operation.Encrypt))
                ret = c.Result;
            return ret;
        }
    }
}
