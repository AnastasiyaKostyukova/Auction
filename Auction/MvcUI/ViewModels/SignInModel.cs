using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcUI.ViewModels
{
    public class SignInModel
    {
        [Required]
        [Display(Name = "Email")]
        [MinLength(5, ErrorMessage = "Must be more then 5")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}