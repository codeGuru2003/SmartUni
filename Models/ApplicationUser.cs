using Microsoft.AspNetCore.Identity;

namespace SmartUni.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? LoginHint { get; set; }
        public string? ImageUrl { get; set; }
    }
}
