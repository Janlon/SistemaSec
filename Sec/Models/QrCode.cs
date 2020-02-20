using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    //[Table("QrCodes", Schema = "dbo")]
    //public class QrCode
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "QrCode", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "QrCode")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
    //    public Guid Codigo { get; set; }

    //    [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Equipamento")]
    //    public int EquipamentosId { get; set; }
    //    public virtual Equipamento Equipamento { get; set; }

    //}
}