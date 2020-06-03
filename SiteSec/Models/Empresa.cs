using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Empresa
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; } = 0;

        public int EmpresaId => Id;

        [Display(Name = "Razão Social", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Razão social")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string RazaoSocial { get; set; } = "";

        [Display(Name = "Nome Fantasia", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Nome de fantasia")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string NomeFantasia { get; set; } = "";

        [Display(Name = "Atividade", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Atividade da empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Atividade { get; set; } = "";

        [Display(Name = "CNPJ", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Número do documento")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string CNPJ { get; set; } = "";

        [Display(Name = "Matriz", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Empresa que são Matriz")]
        public bool EhMatriz { get; set; } = true;



        [Display(Name = "Endereço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Endereço da empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public int EnderecoId { get; set; } = 0;

        [Display(Name = "CEP", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Endereço da empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string CEP { get; set; } = "";

        [Display(Name = "Endereço", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Endereço da empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Logradouro { get; set; } = "";

        public string Complemento { get; set; } = "";

        [Display(Name = "Bairro", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Endereço da empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Bairro { get; set; } = "";

        [Display(Name = "Cidade", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Endereço da empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Localidade { get; set; } = "";

        [Display(Name = "UF", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Endereço da empresa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string UF { get; set; } = "";

        public Endereco Endereco { get; set; }




        public List<Setor> Setores { get; set; }





    }
}