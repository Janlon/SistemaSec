namespace Generics.Helpers
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

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
    }
}