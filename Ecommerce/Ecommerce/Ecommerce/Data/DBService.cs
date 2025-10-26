using Ecommerce.Models;
using MongoDB.Driver;

namespace Ecommerce.Data
{
    public class DBService
    {
        private readonly IMongoDatabase _database;
        private static DBService _instance;

        private DBService(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        // Singleton instance using IOptions
        public static DBService GetInstance(MongoDbSettings settings)
        {
            if (_instance == null)
            {
                _instance = new DBService(settings.ConnectionString, settings.DatabaseName);
            }
            return _instance;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}


