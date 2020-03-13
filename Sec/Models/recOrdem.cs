using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    //[Table("Ordens", Schema = "Sec")]
    //public class Ordem
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Ordem de Serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ordem de Serviço")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
    //    public string Descricao { get; set; }

    //    [Display(Name = "Ativo", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ativo")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [DefaultValue(true)]
    //    public bool Ativo { get; set; } = true;

    //    [ScaffoldColumn(false)]
    //    [NotMapped]
    //    public DateTime DataCadastro
    //    {
    //        get { return dateCreated ?? DateTime.Now; }
    //        set { dateCreated = value; }
    //    }
    //    private DateTime? dateCreated = null;

    //    [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Pessoa")]
    //    public int PessoasId { get; set; }
    //    public virtual Pessoa Pessoa { get; set; }
    //}
}