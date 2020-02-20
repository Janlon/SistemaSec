using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sec.Models
{
    //[Table("Enderecos", Schema = "dbo")]
    //public class Endereco
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [ScaffoldColumn(false)]
    //    public int Id { get; set; }

    //    [Display(Name = "CEP", AutoGenerateField = true, AutoGenerateFilter = true, Prompt ="CEP", Description = "Código de Endereçamento Postal")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(9, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 8)]
    //    public string CEP { get; set; }

    //    [Display(Name = "Estado", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Estado", ShortName ="UF")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [ValidarEstado(ErrorMessage = "Escolha um estado valído")]
    //    [StringLength(2, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
    //    public string Estado { get; set; }

    //    [Display(Name = "Cidade", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cidade")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
    //    public string Cidade { get; set; }

    //    [Display(Name = "Bairro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Bairro")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
    //    public string Bairro { get; set; }

    //    [Display(Name = "Logradouro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt ="Logradouro (rua, avenida, travessa etc)", Description = "Rua, Avenida, Travessa...")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(100, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
    //    public string Logradouro { get; set; }

    //    [Display(Name = "Número", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Número")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [RegularExpression("([1-9][0-9]*)")]
    //    [Range(0, int.MaxValue, ErrorMessage = "Digite um número válido")]
    //    public string Numero { get; set; }

    //    [Display(Name = "Complemento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt ="Complemento", Description = "Casa A, Apt 101...")]
    //    [Required(ErrorMessage = "{0} é obrigatório.")]
    //    [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 1)]
    //    public string Complemento { get; set; }
    //}
}