using Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Services _services;
        public AuthController(IConfiguration configuration, Services services)
        {
            _configuration = configuration;
            _services = services;
        }

        [HttpPost]
        public bool Login(string userid, string password)
        {
            bool isLogin = _services.IsLoginAsync(userid, password);
            return isLogin;
        }
    }
}
