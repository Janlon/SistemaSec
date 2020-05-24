using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Servico
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Serviço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Serviço")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; }

    }
}