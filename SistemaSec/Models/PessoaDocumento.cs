using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaSec.Models
{
    //[Table("PessoasDocumentos", Schema = "dbo")]
    //public class PessoaDocumento
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Documento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Número Documento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(30, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 1)]
    //    public string Numero { get; set; }

    //    [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Pessoa")]
    //    public int PessoasId { get; set; }
    //    public virtual Pessoa Pessoa { get; set; }

    //    [Display(Name = "Documento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Documento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Documento")]
    //    public int DocumentosId { get; set; }
    //    public virtual Documento Documento { get; set; }

    //}
}