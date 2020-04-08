using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Setor
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Empresa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int EmpresaId { get; set; }

        [Display(Name = "Razão Social", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Setor", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Setor")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; }

        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string Sigla { get; set; }

        public virtual Empresa Empresa { get; set; }
    }
}