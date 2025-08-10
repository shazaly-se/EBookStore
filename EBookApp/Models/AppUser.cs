using Microsoft.AspNetCore.Identity;

namespace EBookApp.Models
{
    public class AppUser :IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
