using APIwithJWt.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using APIwithJWt.Services;

namespace YourNamespace.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Dummy user authentication (Replace with real database check)
            if (request.Email == "admin@example.com" && request.Password == "Admin@123")
            {
                var token = _tokenService.GenerateToken("1", request.Email, "Admin");
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }
}
