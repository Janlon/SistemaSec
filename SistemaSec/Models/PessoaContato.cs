using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaSec.Models
{
    //[Table("PessoasContatos", Schema = "dbo")]
    //public class PessoaContato
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Contato", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Número de Contato")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(30, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 1)]
    //    public string Numero { get; set; }

    //    [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Pessoa")]
    //    public int PessoasId { get; set; }
    //    public virtual Pessoa Pessoa { get; set; }

    //    [Display(Name = "Contato", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Contato")]
    //    public int ContatosId { get; set; }
    //    public virtual Contato Contato { get; set; }

    //}
}