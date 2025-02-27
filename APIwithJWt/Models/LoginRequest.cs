
using System.ComponentModel.DataAnnotations;

namespace APIwithJWt.Models
{
    public class LoginRequest
    {
        [Required]
        public string? Email { get; set; }

        public string?  password { get; set; }

    }
}
