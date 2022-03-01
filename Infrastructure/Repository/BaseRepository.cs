using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repository
{
    public abstract class BaseRepository<T>
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private const string DB_Name = "CynetTestDB";

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration.GetConnectionString("LogManagementCon"));
            
        }

        protected abstract string CollectionName { get; }

        protected IMongoCollection<T> GetCollection()
        {
            var collection = _mongoClient.GetDatabase(DB_Name).GetCollection<T>(CollectionName);
            return collection;
        }
    }
}