using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Setor
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public int IdEmpresa { get; set; }

        [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; }

        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string Sigla { get; set; }
    }
}