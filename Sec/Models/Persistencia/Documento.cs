namespace Sec.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Documentos", Schema ="Sec")]
    public class Documento
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [ScaffoldColumn(false)]
        public int TipoDeDocumentoId { get; set; } = 0;

        [ScaffoldColumn(false)]
        public int PessoaId { get; set; } = 0;

        [Display(Name ="Número", AutoGenerateField =true, AutoGenerateFilter =true, Description ="Número", Prompt ="Número do documento")]
        [Required(ErrorMessage ="{0} é obrigatório.")]
        [StringLength(50, ErrorMessage ="{0} deve ter entre {2} e {1} dígitos/caracteres.", MinimumLength =1)]
        [Column(TypeName = "VARCHAR")]
        public string Numero { get; set; } = "0";

        [Display(Name = "Complemento", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Complemento", Prompt = "Complementos do documento (série, seção etc)")]
        [MaxLength(50, ErrorMessage ="{0) deve ter no máximo {1} dígito/caractere.")]
        [Column(TypeName ="VARCHAR")]
        [Required(AllowEmptyStrings =true)]
        public string Serie { get; set; } = "";

        [Display(Name = "Emissão", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Emissão", Prompt = "Data de emissão")]
        [DataType(DataType.Date)]
        public DateTime? Emissao { get; set; } = DateTime.Now;

        [Display(Name = "Validade", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Validade", Prompt = "Data de validade")]
        [DataType(DataType.Date)]
        public DateTime? Validade { get; set; } = DateTime.Now.AddDays(1);

        [Display(Name = "Tipo De Documento", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Tipo de documento", Prompt = "Tipo")]
        [ForeignKey("TipoDeDocumentoId")]
        public virtual TipoDeDocumento TipoDeDocumento { get; set; }

        [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Nome da pessoa", Prompt = "Pessoa")]
        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }

    }
}