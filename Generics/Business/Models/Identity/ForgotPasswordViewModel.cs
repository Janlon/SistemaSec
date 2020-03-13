namespace Generic.Business.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
