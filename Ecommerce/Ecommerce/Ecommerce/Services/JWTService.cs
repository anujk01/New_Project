using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Services
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<JWTService> _logger;

        public JWTService(IConfiguration configuration, ILogger<JWTService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string GenerateToken(string username)
        {
            try
            {
                var key = _configuration["Jwt:Key"];
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];

                if (string.IsNullOrEmpty(key))
                {
                    _logger.LogError("JWT Key is missing in appsettings.json");
                    throw new Exception("JWT Key is missing.");
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating JWT token");
                return null;
            }
        }
    }
}