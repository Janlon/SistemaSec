using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaSec.Models 
{
    [Table("Cargos", Schema = "dbo")]
    public class Cargo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string Ibge { get; set; } = "";

        [Display(Name = "Cargo", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Cargo", Description = "Cargo na Empresa.")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(150, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; }
    }
}