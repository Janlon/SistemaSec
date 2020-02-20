using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaSec.Models
{
    //[Table("ImagensEquipamentos", Schema = "dbo")]
    //public class ImagemEquipamento
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Required]
    //    [Display(Name = "Imagem", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem")]
    //    public string Nome { get; set; }

    //    [ScaffoldColumn(false)]
    //    [NotMapped]
    //    public string Caminho { get; set; }

    //    [ScaffoldColumn(false)]
    //    [NotMapped]
    //    public string Tipo { get; set; }

    //    [ScaffoldColumn(false)]
    //    [NotMapped]
    //    public string Tamanho { get; set; }

    //    [Display(Name = "Ativo", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ativo")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [DefaultValue(true)]
    //    public bool Ativo { get; set; } = true;

    //    [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ForeignKey("Equipamento")]
    //    public int EquipamentosId { get; set; }
    //    public virtual Equipamento Equipamento { get; set; }

    //}
}