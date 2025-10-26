using Ecommerce.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ecommerce.Data
{
    public class Customer
    {
        private readonly IMongoCollection<UserRequest> _users;
        private readonly IConfiguration _configuration;

        public Customer(IOptions<MongoDbSettings> mongoSettings, IConfiguration configuration)
        {
            var dbHelper = DBService.GetInstance(mongoSettings.Value);
            _users = dbHelper.GetCollection<UserRequest>("master_user");
            _configuration = configuration;
        }
        public async Task<bool> AddUser(string userid, string password, string email, string secretkey)
        {
            // Validate the secret key
            if (secretkey != "secretkey123")
                return false;

            // Create the user object
            var request = new UserRequest
            {
                Username = userid,
                Password = password,
                Email = email
            };

            // Insert the user
            await _users.InsertOneAsync(request);

            // Count how many documents are now in the collection
            long count = await _users.CountDocumentsAsync(Builders<UserRequest>.Filter.Empty);

            // Return true if count > 0 (i.e., insertion succeeded)
            return count > 0;
        }
    }
    
}
