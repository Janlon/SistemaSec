namespace Generics.Helpers
{
    namespace Cryptis
    {
        using System;
        using System.IO;
        using System.Security.Cryptography;

        // Exemplos da Microsoft.
        //private byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        //{
        //    if (plainText == null || plainText.Length <= 0)
        //        throw new ArgumentNullException("plainText");
        //    if (Key == null || Key.Length <= 0)
        //        throw new ArgumentNullException("Key");
        //    if (IV == null || IV.Length <= 0)
        //        throw new ArgumentNullException("IV");
        //    byte[] encrypted;
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = Key;
        //        aesAlg.IV = IV;
        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                swEncrypt.Write(plainText);
        //            encrypted = msEncrypt.ToArray();
        //        }
        //    }
        //    return encrypted;
        //}
        //private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        //{
        //    if (cipherText == null || cipherText.Length <= 0)
        //        throw new ArgumentNullException("cipherText");
        //    if (Key == null || Key.Length <= 0)
        //        throw new ArgumentNullException("Key");
        //    if (IV == null || IV.Length <= 0)
        //        throw new ArgumentNullException("IV");
        //    string plaintext = null;
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = Key;
        //        aesAlg.IV = IV;
        //        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        //        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        //        {
        //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                plaintext = srDecrypt.ReadToEnd();
        //        }
        //    }
        //    return plaintext;
        //}


        public static class Text
        {

            // Chave SALT fixa:
            private static byte[] Salt
            {
                get
                {
                    return new byte[] {
                            128, 125, 122, 99,
                            120, 12, 44, 66,
                            82, 100, 15, 8,
                            64, 99, 19, 10 };
                }
            }

            /// <summary>
            /// Criptografa este texto utilizando AES.
            /// </summary>
            /// <param name="plainText">Este texto.</param>
            /// <param name="sharedSecret">Chave criptográfica.</param>
            public static string AESEncrypt(this string plainText, string sharedSecret)
            {
                if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException("plainText");
                if (string.IsNullOrEmpty(sharedSecret)) throw new ArgumentNullException("sharedSecret");
                // Retorno
                string outStr = null;
                RijndaelManaged aesAlg = null;
                try
                {
                    // Gera a chave com base no texto e no SALT:
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, Salt);
                    // Algoritimo:
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    // Fluxo de criptografia:
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        // Incluindo a IV
                        msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            swEncrypt.Write(plainText);
                        outStr = Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
                finally { if (aesAlg != null) aesAlg.Clear(); }
                return outStr;
            }
            /// <summary>
            /// Decriptografa este texto utilizando AES.
            /// </summary>
            /// <param name="cipherText">Este texto.</param>
            /// <param name="sharedSecret">Chave criptográfica.</param>
            public static string AESDecrypt(string cipherText, string sharedSecret)
            {
                if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException("cipherText");
                if (string.IsNullOrEmpty(sharedSecret)) throw new ArgumentNullException("sharedSecret");
                RijndaelManaged aesAlg = null;
                string plaintext = null;
                try
                {
                    Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, Salt);
                    byte[] bytes = Convert.FromBase64String(cipherText);
                    using (MemoryStream msDecrypt = new MemoryStream(bytes))
                    {
                        aesAlg = new RijndaelManaged();
                        aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                        aesAlg.IV = ReadByteArray(msDecrypt);
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
                finally { if (aesAlg != null) aesAlg.Clear(); }
                return plaintext;
            }
            /// <summary>
            /// Retorna a matriz de bytes do fluxo indicado.
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            private static byte[] ReadByteArray(Stream stream)
            {
                byte[] rawLength = new byte[sizeof(int)];
                if (stream.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
                    throw new SystemException("Stream did not contain properly formatted byte array");
                byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
                if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                    throw new SystemException("Did not read byte array properly");
                return buffer;
            }
        }
    }
}
