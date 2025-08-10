using System.ComponentModel.DataAnnotations;

namespace EBookApp.ViewModel
{
    public class LoginVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
