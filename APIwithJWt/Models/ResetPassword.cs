
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.MicrosoftExtensions;

namespace APIwithJWt.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Email  Required")]
        [EmailAddress(ErrorMessage = "Invalid mail")]
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength (12,MinimumLength =8 ,ErrorMessage="Password Must be between 12 to 8")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$",
        ErrorMessage = "Password must contain at least one uppercase, one lowercase, one number, and one special character.")]
        public string Password { get; set; }

        public string newpassword { get; set; }

        public string confirmpassword { get; set; }

        public string Token { get; set; }

    }
}
