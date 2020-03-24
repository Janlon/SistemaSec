namespace Generics
{
    #region Espaços
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    #endregion

    /// <summary>
    /// Classe para manutenção de erros.
    /// </summary>
    public class ErrorBlock
    {
        [JsonProperty("DetectionDate")]
        [Display(Name = "Data da detecção")]
        public DateTime DetectionDate { get; set; } = DateTime.Now;

        [JsonProperty("Caller")]
        [Display(Name = "Método chamador")]
        public string Caller { get; set; } = "";

        [JsonProperty("FileName")]
        [Display(Name = "Arquivo de código")]
        public string FileName { get; set; } = "";

        [JsonProperty("LineNumber")]
        [Display(Name = "Linha do arquivo")]
        public int LineNumer { get; set; } = 0;

        [JsonProperty("ErrorMessage")]
        [Display(Name = "Mensagem de erro")]
        public string ErrorMessage { get; set; } = "";

        [JsonProperty("JSonError")]
        [Display(Name = "Erro como JSon")]
        [ScaffoldColumn(false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual string JSonError { get; set; } = "";
    }
}
