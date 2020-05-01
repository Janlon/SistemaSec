using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Usuario
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [JsonProperty("pessoa")]
        public string Pessoa { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [Display(Name = "Pessoa", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Pessoa")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [JsonProperty("pessoaId")]
        public long PessoaId { get; set; }


        [Display(Name = "Permissão", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Permissão")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public List<long> RegraId { get; set; }

        [Display(Name = "Permissão", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Permissão")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public List<string> RegraName { get; set; }


        [Display(Name = "Senha")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(255, ErrorMessage = "{0} Deve ter entre {2} e {1} caracteres", MinimumLength = 5)]
        [JsonProperty("senha")]
        public string Senha { get; set; }


        [Display(Name = "Confirme a Senha")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(255, ErrorMessage = "{0} Deve ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string ConfirmeSenha { get; set; }

    }
}