using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sec.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
