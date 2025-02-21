

using System.ComponentModel.DataAnnotations;

namespace APIwithJWt.Models
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        public String password { get; set; }

    }
}
