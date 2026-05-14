using project001backend.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace project001backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/api")]
    public class FeatureController : ControllerBase
    {
        private readonly ILogger<FeatureController> _logger;
        private readonly Customer _customer;

        public FeatureController(ILogger<FeatureController> logger, Customer customer)
        {
            _logger = logger;
            _customer = customer;
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser(string username, string password, string email, string secretkey)
        {
            try
            {
                var user = await _customer.AddUser(username, password, email, secretkey);
                if (!user) return Unauthorized(new { message = "Invalid Credentials or Secret", code = 400 });
                return Ok(new
                {
                    message = "User added successfully!!",
                    code = 200
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user");
                return StatusCode(500, new { message = "Internal server error", code = 500 });
            }

        }
    }
}


