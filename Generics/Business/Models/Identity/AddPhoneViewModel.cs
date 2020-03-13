namespace Generic.Business.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número de telefone")]
        public string Number { get; set; }
    }
}