using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Pessoa 
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

        [Display(Name = "Ocupação", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Ocupação")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Atividade { get; set; } = "";

        [Display(Name = "CPF", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Número do Documento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string CPF { get; set; } = "";



        [Display(Name = "Imagem", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Imagem")]
        public byte[] Files { get; set; }


        [ScaffoldColumn(false)]
        public List<Imagem> Imagens { get; set; }


        public int DocumentoId { get; set; }
        public Documento documento { get; set; }

    }
}


