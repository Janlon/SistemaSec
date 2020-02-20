namespace Sec.QRCoder
{
    namespace Exceptions
    {

        using System;

        /// <summary>
        /// Dados maiores do que a capacidade de contenção do QRCode.
        /// </summary>
        [Serializable]
        public class DataTooLongException : Exception
        {
            private const string errMessage = @"Os dados excedem o tamanho máximo de um QR code padrão. Para os parâmetros escolhidos o tamanho máximo seria: (ECC nível={0}, EncodingMode={1}) até {2} bytes.";
            public DataTooLongException(string eccLevel, string encodingMode, int maxSizeByte) : base(string.Format(errMessage, eccLevel, encodingMode, maxSizeByte)) { }
            public DataTooLongException() { }
            public DataTooLongException(string message) : base(message) { }
            public DataTooLongException(string message, Exception innerException) : base(message, innerException) { }
            protected DataTooLongException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
        }
    }
}