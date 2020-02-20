using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    //[Table("PessoasCargos", Schema = "dbo")]
    //public class PessoaCargo
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Pessoa")]
    //    public int PessoasId { get; set; }
    //    public virtual Pessoa Pessoa { get; set; }

    //    [Display(Name = "Cargo", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cargo")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Cargo")]
    //    public int CargosId { get; set; }
    //    public virtual Cargo Cargo { get; set; }
    //}
}