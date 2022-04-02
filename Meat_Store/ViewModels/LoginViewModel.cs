using System.ComponentModel.DataAnnotations;

namespace Meat_Store.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name ="Запам'ятати")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
