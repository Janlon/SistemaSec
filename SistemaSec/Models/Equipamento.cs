using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaSec.Models
{
    //[Table("Equipamentos", Schema = "dbo")]
    //public class Equipamento
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "Equipamento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Equipamento", Description = "Nome do Equipamento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
    //    public string Descricao { get; set; }

    //    [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla", Description ="Sigla do Equipamento")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
    //    public string Sigla { get; set; }

    //    [Display(Name = "Quantidade", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Quantidade", Description = "Quantidade de Equipamentos")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [RegularExpression("([1-9][0-9]*)")]
    //    [Range(0, int.MaxValue, ErrorMessage = "Digite um número válido")]
    //    public int Quantidade { get; set; }

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
    //}
}