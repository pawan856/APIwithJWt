using APIwithJWt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using APIwithJWt.Models;
using APIwithJWt.Data;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace APIwithJWT.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly EmailService _emailService;
        private readonly UserManager<User> _userManager;
        public AuthController(ITokenService tokenService, AppDbContext context,ILogger logger, EmailService emailService)
        {
            _tokenService = tokenService;
            _context = context;
            _emailService = emailService;
            _logger = logger;
            _userManager = UserManager;
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


        [HttpPost("Forget-Password")]
        public async Task<IActionResult> ForgotPassword([FromBody] forgetPasswordRequest model)
        {
            var user = await _usermanager.FindByEmailasync(model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            var usertoken = await _usermanager.GenratePasswordReset.Async(user);
            var resetLink = $"{_configuration["FrontendUrl"]}/reset-password?token={token}&email={user.Email}";            
            await _emailservice.SendEmailAsync(user.email, "Reset Password", $"click  <a herf ='{resetLink}'here </a> to reset your password");
            return Ok("Password Reset link has been sent to your mail");
        }

        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {

            var user = await _userManager.FindByEmailasync(model.Email);
            {
                if (user == null)
                    return BadRequest(new { message = "User not found" });
            }
            var userresult = await _usermanager.Resetpasswordasync(user, model.Token, model.Newpassword);
            if (!userresult.Succeeded)
            {
                return BadRequest(userresult.errors);
            }
            return Ok(new { message = "Password hasbeen Reset Successfully" });

        }

    }
}

