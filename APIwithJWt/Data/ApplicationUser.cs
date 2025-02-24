using Microsoft.AspNetCore.Identity;

namespace APIwithJWt.Data
{
    public class ApplicationUser : IdentityUser 
    {
        public string? fullname { get; set; } 
        
             
    }
}
