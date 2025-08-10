using System.ComponentModel.DataAnnotations;

namespace EBookApp.ViewModel
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
