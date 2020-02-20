using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaSec.Models
{

    /// <summary>
    /// Vamos partir do seguinte princípio: Documento resume-se, neste aplicativo, 
    /// apenas aos tipos de documentos que identiicam uma pessoa, seja ela física ou 
    /// jurídica. Assim, um documento só poderá pertencer á uma única pessoa.
    /// Por exemplo, uma ordem de serviço (documento). 
    /// </summary>
    [Table("TiposDeDocumentos", Schema = "dbo")]
    public class TipoDeDocumento
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// Tipo do documento.
        /// </summary>
        [Display(Name = "Tipo de documento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Tipo de documento", Description = "RG, CPF, CNPJ...")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; }

        /// <summary>
        /// Sigla do documento.
        /// </summary>
        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla", Description = "Sigla do documento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string Sigla { get; set; }
    }


    ///// <summary>
    ///// Identificação do registro de pessoa.
    ///// </summary>
    //[ScaffoldColumn(false)]
    //public int PessoaId { get; set; }
    ///// <summary>
    ///// Pessoas do documento.
    ///// </summary>
    //[ForeignKey("PessoaId")]
    //[Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoas do documento", Description = "Detentor do documento")]
    //public virtual Pessoa Pessoa { get; set; }
}