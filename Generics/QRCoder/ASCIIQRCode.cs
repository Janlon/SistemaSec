namespace Generics.QRCoder
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AsciiQRCode : AbstractQRCode, IDisposable
    {
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public AsciiQRCode() { }

        public AsciiQRCode(QRCodeData data) : base(data) { }

        /// <summary>
        /// Retorna o texto que contém o código QR como um texto ASCII.
        /// </summary>
        /// <param name="repeatPerModule">Número de ciclos darkColorString/whiteSpaceString por módulo.</param>
        public string GetGraphic(int repeatPerModule)
        {
            return string.Join("\n", GetLineByLineGraphic(repeatPerModule));
        }


        /// <summary>
        /// Retorna o texto que contém o código QR como um texto ASCII.
        /// </summary>
        /// <param name="repeatPerModule">Número de ciclos darkColorString/whiteSpaceString por módulo.</param>
        /// <param name="darkColorString">Texto para a cor escura. Caso seja um texto, whiteSpaceString deve ser do mesmo tmanho.</param>
        /// <param name="whiteSpaceString">Texto para a cor clara. Caso seja um texto, darkColorString deve ser do mesmo tmanho.</param>
        /// <param name="endOfLine">Separador de linha. Padrão como "\n".</param>
        public string GetGraphic(int repeatPerModule, string darkColorString, string whiteSpaceString, string endOfLine = "\n")
        {
            return string.Join(endOfLine, GetLineByLineGraphic(repeatPerModule, darkColorString, whiteSpaceString));
        }


        /// <summary>
        /// Retorna a matriz de linhas de código QR como ASCII.
        /// </summary>
        /// <param name="repeatPerModule">Número de ciclos darkColorString/whiteSpaceString por módulo.</param>
        public string[] GetLineByLineGraphic(int repeatPerModule)
        {
            return GetLineByLineGraphic(repeatPerModule, "██", "  ");
        }


        /// <summary>
        /// Retorna a matriz de linhas de código QR como ASCII.
        /// </summary>
        /// <param name="repeatPerModule">Número de ciclos darkColorString/whiteSpaceString por módulo.</param>
        /// <param name="darkColorString">Texto para a cor escura. Caso seja um texto, whiteSpaceString deve ser do mesmo tmanho.</param>
        /// <param name="whiteSpaceString">Texto para a cor clara. Caso seja um texto, darkColorString deve ser do mesmo tmanho.</param>
        public string[] GetLineByLineGraphic(int repeatPerModule, string darkColorString, string whiteSpaceString)
        {
            var qrCode = new List<string>();
            var adjustmentValueForNumberOfCharacters = darkColorString.Length / 2 != 1 ? darkColorString.Length / 2 : 0;
            var verticalNumberOfRepeats = repeatPerModule + adjustmentValueForNumberOfCharacters;
            var sideLength = QrCodeData.ModuleMatrix.Count * verticalNumberOfRepeats;
            for (var y = 0; y < sideLength; y++)
            {
                bool emptyLine = true;
                var lineBuilder = new StringBuilder();

                for (var x = 0; x < QrCodeData.ModuleMatrix.Count; x++)
                {
                    var module = QrCodeData.ModuleMatrix[x][(y + verticalNumberOfRepeats) / verticalNumberOfRepeats - 1];
                    for (var i = 0; i < repeatPerModule; i++)
                        lineBuilder.Append(module ? darkColorString : whiteSpaceString);
                    if (module)
                        emptyLine = false;
                }
                if (!emptyLine)
                    qrCode.Add(lineBuilder.ToString());
            }
            return qrCode.ToArray();
        }
    }    
}


