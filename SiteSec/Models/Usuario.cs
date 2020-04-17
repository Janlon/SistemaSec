using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Usuario
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        [Display(Name = "Nome completo", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Nome Completo")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Nome { get; set; } = "";

        [Display(Name = "Email", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email não é válido")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Email { get; set; } = "";

    }
}