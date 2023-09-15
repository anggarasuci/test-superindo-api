using ProductApi.Data.Entity;
using MongoDB.Driver;
using ProductApi.Util;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace ProductApi.Data.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IMongoCollection<ProductCategoryEntity> productCategoryCollection;
        private readonly IMongoCollection<TransactionEntity> transactionCollection;
        public TransactionService(IOptions<DBSetting> databaseSetting)
        {
            var mongoClient = new MongoClient(databaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSetting.Value.DatabaseName);
            productCategoryCollection = mongoDatabase.GetCollection<ProductCategoryEntity>(databaseSetting.Value.ProductCollectionName);
            transactionCollection = mongoDatabase.GetCollection<TransactionEntity>(databaseSetting.Value.TransactionCollectionName);
        }

        public async Task AddTransaction(TransactionEntity transactionEntity)
        {
            var productCategory = await productCategoryCollection.Find(x => x.Id == transactionEntity.ProductCategoryId).FirstOrDefaultAsync();
            transactionEntity.ProductCategoryName = productCategory.Name;

            if (transactionEntity.Id != null)
            {
                await transactionCollection.ReplaceOneAsync(x => x.Id == transactionEntity.Id, transactionEntity);
            }
            else
            {
                await transactionCollection.InsertOneAsync(transactionEntity);
            }
            
        }

        public async Task DeleteTransaction(string transactionId)
        {
            await transactionCollection.DeleteOneAsync(x => x.Id == transactionId);
        }

        public async Task<TransactionEntity> GetTransactionByIdAsync(string transactionId)
        {
            return await transactionCollection.Find(x => x.Id == transactionId).FirstOrDefaultAsync();
        }

        public async Task<List<TransactionEntity>> GetTransactions()
        {
            return await transactionCollection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateTransaction(TransactionEntity transactionEntity)
        {
            await transactionCollection.ReplaceOneAsync(x => x.Id == transactionEntity.Id, transactionEntity);
        }
    }
}

