﻿namespace Sec.Models
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
    [Table("TiposDeDocumentos", Schema = "Sec")]
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
        [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [MaxLength(100)]
        [Column("Descricao", TypeName = "VARCHAR")]
        public string Descricao { get; set; }

        /// <summary>
        /// Sigla do documento.
        /// </summary>
        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla", Description = "Sigla do documento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        [MaxLength(10)]
        [Column("Sigla", TypeName = "VARCHAR")]
        public string Sigla { get; set; }

        /// <summary>
        /// Destino do tipo de documento.
        /// </summary>
        [Display(Name = "Pessoa física", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa fisica", Description = "Se aplica à pessoas físicas?")]
        [DefaultValue(true)]
        public bool PessoaFisica { get; set; } = true;

        /// <summary>
        /// Indica de este item identifica uma pessoa.
        /// </summary>
        [Display(Name = "Identificador", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Identificador", Description = "É um documento para identificação?")]
        [DefaultValue(true)]
        public bool Identificador { get; set; } = true;

    }
}