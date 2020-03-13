using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Generic.Models
{
    //[Table("EquipamentosSetores", Schema = "Sec")]
    //public class EquipamentoSetor
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Equipamento")]
    //    public int EquipamentosId { get; set; }
    //    public virtual Equipamento Equipamento { get; set; }

    //    [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Setor")]
    //    public int SetoresId { get; set; }
    //    public virtual Setor Setor { get; set; }
    //}
}