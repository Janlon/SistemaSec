using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public class Usuario
    {
        [ScaffoldColumn(false)]
        public Guid Id => UserId;

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
        public List<Regra> Permissoes { get; set; } = new List<Regra>();

        [Display(Name = "Permissão", AutoGenerateField = true, AutoGenerateFilter = true, Prompt = "Permissão")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public List<string> RegrasName { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserName { get; set; }
        
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(255, ErrorMessage = "{0} Deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "A senha deve conter letras maiúsculas, minúsculas, números e caracteres especiais.")]
        [JsonProperty("senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirme a Senha")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não combinam.")]
        public string ConfirmeSenha { get; set; }


        public string AccessTokenFormat { get; internal set; }

        public Usuario()
        {
            Permissoes = new List<Regra>(); 
        }

    }

}