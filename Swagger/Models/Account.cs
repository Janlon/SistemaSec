using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Swagger.Models
{
    public class Account
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserName { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(255, ErrorMessage = "{0} Deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "A senha deve conter letras maiúsculas, minúsculas, números e caracteres especiais.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}