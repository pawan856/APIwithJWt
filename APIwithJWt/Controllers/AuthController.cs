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

        public AuthController(ITokenService tokenService, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
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

    }
}

