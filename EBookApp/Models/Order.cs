using System.ComponentModel.DataAnnotations;

namespace EBookApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public decimal TotalAmount { get; set; }

        public List<OrderItem> Items { get; set; }
        public string? UserId { get; set; }  // FK to ApplicationUser
        public AppUser? User { get; set; }
    }
}
