namespace Sec
{
    namespace Helpers
    {
        namespace Cripto
        {
            using System;
            using System.IO;
            using System.Security.Cryptography;
            using System.Text;

            /// <summary>
            /// Extensões para criptografia de textos.
            /// </summary>
            public static class CryptoStringExtensions
            {
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
                /// Retorna a parte deste texto composta por letras apenas.
                /// </summary>
                /// <param name="value">Este texto.</param>
                /// <returns>Resultado.</returns>
                public static string JustLetters(this string value)
                {
                    string ret = "";
                    foreach (char c in value)
                        if (char.IsLetter(c))
                            ret += c;
                    return ret;
                }
                /// <summary>
                /// Retorna a parte deste texto composta por números apenas.
                /// </summary>
                /// <param name="value">Este texto.</param>
                /// <returns>Resultado.</returns>
                public static string JustNumbers(this string value)
                {
                    string ret = "";
                    foreach (char c in value)
                        if (char.IsDigit(c))
                            ret += c;
                    return ret;
                }
                /// <summary>
                /// Retorna a parte deste texto composta por letras ou números apenas.
                /// </summary>
                /// <param name="value">Este texto.</param>
                /// <returns>Resultado.</returns>
                public static string JustLettersAndDigits(this string value)
                {
                    string ret = "";
                    foreach (char c in value)
                        if (char.IsLetterOrDigit(c))
                            ret += c;
                    return ret;
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

            /// <summary>
            /// Classe interna para suporte de criptografia.
            /// </summary>
            internal class Crypto : IDisposable
            {
                #region Manutenção
                public enum Operation
                {
                    Encrypt,
                    Decrypt
                }
                private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                private int BlockSize = 128;
                private bool disposedValue;
                private string plain { get; set; } = "";
                private string key { get; set; } = "";
                #endregion

                #region Propriedades
                public string Result { get; internal set; } = "";
                #endregion

                #region Instância        
                public Crypto(string plainText, string keyValue, Operation operation = Operation.Encrypt)
                {
                    disposedValue = false;
                    plain = plainText;
                    key = keyValue;
                    DoTheJob(operation);
                }
                private void CleanUp() { }
                protected virtual void Dispose(bool disposing)
                {
                    if (!disposedValue)
                    {
                        if (disposing) { CleanUp(); }
                        disposedValue = true;
                    }
                }
                ~Crypto() { Dispose(false); }
                public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
                #endregion

                #region Métodos de suporte
                private void DoTheJob(Operation operation)
                {
                    if (operation == Operation.Encrypt)
                    { Encrypt(); }
                    else
                    { Decrypt(); }
                }
                private void Encrypt()
                {
                    if (plain == "") return;
                    byte[] bytes = Encoding.Unicode.GetBytes(plain);
                    SymmetricAlgorithm crypt = Aes.Create();
                    HashAlgorithm hash = MD5.Create();
                    crypt.BlockSize = BlockSize;
                    crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(key));
                    crypt.IV = IV;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                            cryptoStream.Write(bytes, 0, bytes.Length);
                        Result = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
                private void Decrypt()
                {
                    if (plain == "") return;
                    byte[] bytes = Convert.FromBase64String(plain);
                    SymmetricAlgorithm crypt = Aes.Create();
                    HashAlgorithm hash = MD5.Create();
                    crypt.Key = hash.ComputeHash(Encoding.Unicode.GetBytes(key));
                    crypt.IV = IV;
                    using (MemoryStream memoryStream = new MemoryStream(bytes))
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] decryptedBytes = new byte[bytes.Length];
                        cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                        Result = Encoding.Unicode.GetString(decryptedBytes);
                    }
                }
                #endregion

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
            }
        }
    }
}