using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    //[Table("Contatos", Schema = "dbo")]
    //public class Contato
    //{
    //    public Contato()
    //    {
    //        Pessoa = new HashSet<Pessoa>();
    //    }

    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Tipo de Contato", AutoGenerateField = true, AutoGenerateFilter = true, Prompt ="Tipo de Contato", Description = "Telefone, WhatsApp...")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
    //    public string Descricao { get; set; }

    //    public virtual HashSet<Pessoa> Pessoa { get; set; }
    //}
}