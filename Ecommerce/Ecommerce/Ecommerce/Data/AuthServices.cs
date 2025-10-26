using Ecommerce.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ecommerce.Data
{
    public class AuthServices
    {
        private readonly IMongoCollection<UserRequest> _users;
        private readonly IConfiguration _configuration;

        public AuthServices(IOptions<MongoDbSettings> mongoSettings, IConfiguration configuration)
        {
            var dbHelper = DBService.GetInstance(mongoSettings.Value);
            _users = dbHelper.GetCollection<UserRequest>("master_user");
            _configuration = configuration;
        }

        public async Task<UserRequest> GetUserByUsername(string username, string password)
        {
            return await _users.Find(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }
    }
}
