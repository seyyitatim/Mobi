using System.ComponentModel.DataAnnotations;

namespace Mobi.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email zorunludur.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email Adresiniz doğru formatta değildir.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password zorunludur.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
