namespace Sec.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Vamos partir do seguinte princípio: Documento resume-se, neste aplicativo, 
    /// apenas aos tipos de documentos que identiicam uma pessoa, seja ela física ou 
    /// jurídica. Assim, um documento só poderá pertencer á uma única pessoa.
    /// Por exemplo, uma ordem de serviço (documento). 
    /// </summary>
    [Table("TiposDeSetores", Schema = "Sec")]
    public class TipoDeSetor
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        /// <summary>
        /// Tipo do setor.
        /// </summary>
        [Display(Name = "Tipo de setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Tipo de setor", Description = "UNIDADE DE TERAPIA INTENSIVA, FARMÁCIA...")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [MaxLength(100)]
        [Column("Descricao", TypeName = "VARCHAR")]
        public string Descricao { get; set; }


        /// <summary>
        /// Sigla do setor.
        /// </summary>
        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla", Description = "UTI, FARM...")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        [MaxLength(10)]
        [Column("Sigla", TypeName = "VARCHAR")]
        public string Sigla { get; set; }
  

    }
}