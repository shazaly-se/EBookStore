using System.ComponentModel.DataAnnotations;

namespace EBookApp.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Biography is required")]
        [Display(Name = "Biography")]
        public string? Bio { get; set; }
        [Display(Name = "Profile Picture")]
        public string? ProfilePicture { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
