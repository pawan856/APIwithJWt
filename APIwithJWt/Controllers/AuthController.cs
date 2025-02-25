using APIwithJWt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using APIwithJWt.Models;
using APIwithJWt.Data;


namespace APIwithJWT.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        public AuthController(ITokenService tokenService, AppDbContext context, ILogger logger, SignInManager<APIwithJWt.Data.ApplicationUser> signInManager,IConfiguration configuration, IEmailService emailService, UserManager<User> userManager)
        {
            _tokenService = tokenService;
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest Login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Login.Email);


            if (User == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });

            }

            if (!BCrypt.Net.BCrypt.Verify(Login.password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid credentials " });
            }

            var token = _tokenService.GenerateToken(user.ID.ToString(), Login.Email, "user");
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Registration request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest(new { message = "Email already exists." });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Fullname = request.Fullname,
                Email = request.Email,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }
        public interface IEmailService
        {
            Task SendEmailAsync(string emial, string subject, string htmlMessage);
        }

        [HttpPost("Forget-Password")]
        public async Task<IActionResult> ForgotPassword([FromBody] forgetPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            var userToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{_configuration["FrontendUrl"]}/reset-password?token={userToken}&email={user.Email}";
            await _emailService.SendEmailAsync(user.Email, "Reset Password", $"Click <a href='{resetLink}'>here</a> to reset your password");
            return Ok("Password reset link has been sent to your email");
        }

       
        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            var userResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!userResult.Succeeded)
            {
                return BadRequest(userResult.Errors);
            }
            return Ok(new { message = "Password has been reset successfully" });
        }

        


    }
}

