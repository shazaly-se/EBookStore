using System.ComponentModel.DataAnnotations;

namespace EBookApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title  is required")]
      
        public string? Title { get; set; }
        [Required(ErrorMessage = "Price  is required")]
        public decimal Price { get; set; }

        public string? Description { get; set; }
        [Display(Name = "Cover Image")]
        public string? CoverImage { get; set; }
        [Display(Name = "Author")]
        [Required(ErrorMessage = "Author is required")]
        public int? AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}
