using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Емаил")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Емаил")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди лозинка")]
        [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Името е задолжително")]
        [StringLength(12, ErrorMessage = "Максималната големина на името треба да е 12 карактери")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Презимето е задолжително")]
        [StringLength(20, ErrorMessage = "Максималната големина на презимето треба да е 20 карактери")]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Адресата е задолжителна")]
        //[RegularExpression(@"^[a-zA-Z]{2,20} \d{1,4}$", ErrorMessage = "ИмеАдреса број е правилниот формат кој треба да го внесете")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }


        [Display(Name = "Години")]
        [Required(ErrorMessage = "Годините се задолжителни")]
        [Range(18, 99, ErrorMessage = "Сопственикот треба да биде полнолетен")]
        public int Age { get; set; }

        public List<string> types { get; set; }

        [Display(Name = "Тип на корисник")]
        public string type { get; set; }

        public RegisterViewModel()
        {
            types = new List<string>();
        }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Емаил")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди лозинка")]
        [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Емаил")]
        public string Email { get; set; }
    }
}
