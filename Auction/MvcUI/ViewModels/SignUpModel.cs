using System.ComponentModel.DataAnnotations;
namespace MvcUI.ViewModels
{
    public class SignUpModel: SignInModel
    {
        [Required]
        [Display(Name = "User name")]
        [MaxLength(30, ErrorMessage = "Should be less than 30 characters")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again!")]
        public string ConfirmPassword { get; set; }
    }
}