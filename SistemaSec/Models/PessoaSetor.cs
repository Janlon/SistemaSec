using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaSec.Models
{
    //[Table("PessoasSetores", Schema = "dbo")]
    //public class PessoaSetor
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Pessoa")]
    //    public int PessoasId { get; set; }
    //    public virtual Pessoa Pessoa { get; set; }

    //    [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Setor")]
    //    public int SetoresId { get; set; }
    //    public virtual Setor Setor { get; set; }
    //}
}