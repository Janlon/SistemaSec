namespace Generics.QRCoder
{
    /// <summary>
    /// Classe para propiciar as heranças.
    /// </summary>
    public abstract class AbstractQRCode
    {
        /// <summary>
        /// Manutenção dos dados.
        /// </summary>
        protected QRCodeData QrCodeData { get; set; }

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        protected AbstractQRCode() {        }

        /// <summary>
        /// Construtor parametrizado com os dados.
        /// </summary>
        /// <param name="data">Dados da instância.</param>
        protected AbstractQRCode(QRCodeData data) { this.QrCodeData = data; }

        /// <summary>
        /// Configura os dados após a instância ter sido criada.
        /// </summary>
        /// <param name="data">Dados para a instância.</param>
        virtual public void SetQRCodeData(QRCodeData data) { this.QrCodeData = data; }

        /// <summary>
        /// Destruidor padrão.
        /// </summary>
        public void Dispose()
        {
            this.QrCodeData?.Dispose();
            this.QrCodeData = null;
        }
    }
}