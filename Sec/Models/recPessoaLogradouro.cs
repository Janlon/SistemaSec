using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    //[Table("PessoasLogradouros", Schema = "Sec")]
    //public class PessoaLogradouro
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Pessoa")]
    //    public int PessoasId { get; set; }
    //    public virtual Pessoa Pessoa { get; set; }

    //    [Display(Name = "Logradouro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Logradouro")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Logradouro")]
    //    public int LogradourosId { get; set; }
    //    public virtual Logradouro Logradouro { get; set; }
    //}
}