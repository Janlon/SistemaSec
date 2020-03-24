namespace Generics
{
    #region Espaços
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    #endregion

    /// <summary>
    /// Para manter log de ações junto ao banco de dados.
    /// </summary>
    [JsonObject("ActionBlock")]
    public class ActionBlock
    {
        /// <summary>
        /// Método que efetuou a chamada onde o erro foi detectado.
        /// </summary>
        [JsonProperty("Caller")]
        [Display(Name = "Método chamador")]
        public string Caller { get; internal set; } = "";

        /// <summary>
        /// Nome do arquivo (código fonte) onde o erro foi detectado.
        /// </summary>
        [JsonProperty("FileName")]
        [Display(Name = "Arquivo de código")]
        public string FileName { get; internal set; } = "";

        /// <summary>
        /// Data e hora da detecção do erro.
        /// </summary>
        [JsonProperty("ExecutionDate")]
        [Display(Name = "Data da execução")]
        public DateTime ExecutionDate { get; internal set; } = DateTime.MinValue;

        /// <summary>
        /// Mensagem de erro.
        /// </summary>
        [JsonProperty("Message")]
        [Display(Name = "Mensagem de erro")]
        public string Message { get; internal set; } = "";

        /// <summary>
        /// Linha do arquivo (código fonte) onde o erro foi detectado.
        /// </summary>
        [JsonProperty("LineNumber")]
        [Display(Name = "Linha do arquivo")]
        public int LineNumer { get; internal set; } = 0;


        [JsonProperty("JSonError")]
        [Display(Name = "Erro como JSon")]
        [ScaffoldColumn(false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual string JSonError { get; set; } = "";
    }
}
