using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaSec.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
