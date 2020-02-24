using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{

    [Table("Documentos", Schema ="dbo")]
    public class Documento
    {
        /// <summary>
        /// Identificação do registro.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int TipoDeDocumentoId { get; set; }

        [ScaffoldColumn(false)]
        public int PessoaId { get; set; }


        [Display(Name ="Número", AutoGenerateField =true, AutoGenerateFilter =true, Description ="Número", Prompt ="Número do documento")]
        [Required(ErrorMessage ="{0} é obrigatório.")]
        public string Numero { get; set; }

        [Display(Name = "Complemento", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Complemento", Prompt = "Complementos do documento (série, seção etc)")]
        [Required(AllowEmptyStrings =true)]
        public string Serie { get; set; } = "";

        [Display(Name = "Emissão", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Emissão", Prompt = "Data de emissão")]
        [DataType(DataType.Date)]
        public DateTime Emissao { get; set; } = DateTime.Now;

        [Display(Name = "Validade", AutoGenerateField = true, AutoGenerateFilter = true, Description = "Validade", Prompt = "Data de validade")]
        [DataType(DataType.Date)]
        public DateTime? Validade { get; set; }

        [ForeignKey("TipoDeDocumentoId")]
        public virtual TipoDeDocumento Tipo { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa Pessoa { get; set; }
    }
}