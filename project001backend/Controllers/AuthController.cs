using project001backend.Data;
using project001backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace project001backend.Controllers
{
    [ApiController]
    [Route("[controller]/api")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthServices _services;
        private readonly JWTService _jwtService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IConfiguration configuration, AuthServices services, JWTService jwtService, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _services = services;
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _services.GetUserByUsername(username, password);
            if (user == null || user.Password != password)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _jwtService.GenerateToken(user.Username);
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Token generation failed for user: {Username}", user.Username);
                return StatusCode(500, new { message = "Internal server error" });
            }
            return Ok(new
            {
                token,
                user = new { user.Username, user.Email }
            });
        }
    }
}
