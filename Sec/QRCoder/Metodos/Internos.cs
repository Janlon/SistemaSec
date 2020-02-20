namespace Sec.QRCoder
{

    using System;

    /// <summary>
    /// M�todos auxiliares internos.
    /// </summary>
    internal static class StaticMethods
    {
        /// <summary>
        /// IsNullOrWhiteSpace.
        /// </summary>
        /// <param name="value">Valor textual.</param>
        /// <returns>
        /// <c>true</c> quando <paramref name="value"/> � nulo ou est� em branco; Caso contr�rio, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(String value)
        {
            if (value == null) return true;

            for (int i = 0; i < value.Length; i++)
            {
                if (!Char.IsWhiteSpace(value[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// ReverseString.
        /// </summary>
        /// <param name="value">Valor textual.</param>
        /// <returns>Texto reverso.</returns>
        public static string ReverseString(string value)
        {
            char[] chars = value.ToCharArray();
            char[] result = new char[chars.Length];
            for (int i = 0, j = value.Length - 1; i < value.Length; i++, j--)
                result[i] = chars[j];
            return new string(result);
        }

        /// <summary>
        /// IsAllDigit.
        /// </summary>
        /// <param name="value">Valor textual.</param>
        /// <returns>Retorna se � ou n�o totalmente composto por d�gitos.</returns>
        public static bool IsAllDigit(string value)
        {
            foreach (var c in value)
                if (!char.IsDigit(c))
                    return false;
            return true;
        }
    }
}