using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class TipoDocumento
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Documento", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Tipo de documento", Description = "RG, CPF, CNPJ...")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(50, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        public string Descricao { get; set; }

        [Display(Name = "Sigla", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Sigla", Description = "Sigla do documento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(10, ErrorMessage = " {0} deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string Sigla { get; set; }

        [Display(Name = "Pessoa física", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa fisica", Description = "Se aplica à pessoas físicas?")]
        public bool PessoaFisica { get; set; } = true;

        [Display(Name = "Identificador", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Identificador", Description = "É um documento para identificação?")]
        public bool Identificador { get; set; } = true;

    }

}