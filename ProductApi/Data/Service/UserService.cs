using ProductApi.Data.Entity;
using MongoDB.Driver;
using ProductApi.Util;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace ProductApi.Data.Service
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<UserEntity> userCollection;
        public UserService(IOptions<DBSetting> databaseSetting)
        {
            var mongoClient = new MongoClient(databaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSetting.Value.DatabaseName);
            userCollection = mongoDatabase.GetCollection<UserEntity>(databaseSetting.Value.UserCollectionName);
        }

        public async Task AddUserAsync(UserEntity user)
        {
            await userCollection.InsertOneAsync(user);
        }

        public async Task<UserEntity> LoginUserAsync(string username, string password)
        {
            return await userCollection.Find(x => x.Username == username &&
            x.Password == password).FirstOrDefaultAsync();

        }
    }
}

