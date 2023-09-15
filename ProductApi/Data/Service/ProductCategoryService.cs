using ProductApi.Data.Entity;
using MongoDB.Driver;
using ProductApi.Util;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace ProductApi.Data.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IMongoCollection<ProductCategoryEntity> productCategoryCollection;
        public ProductCategoryService(IOptions<DBSetting> databaseSetting)
        {
            var mongoClient = new MongoClient(databaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSetting.Value.DatabaseName);
            productCategoryCollection = mongoDatabase.GetCollection<ProductCategoryEntity>(databaseSetting.Value.ProductCollectionName);
        }

        public async Task AddProductCategoryAsync(ProductCategoryEntity productCategory)
        {
            if (productCategory.Id != null)
            {
                await productCategoryCollection.ReplaceOneAsync(x => x.Id == productCategory.Id, productCategory);
            }
            else
            {
                await productCategoryCollection.InsertOneAsync(productCategory);
            }
        }

        public async Task DeleteProductCategoryAsync(string productCategoryId)
        {
            await productCategoryCollection.DeleteOneAsync(x => x.Id == productCategoryId);
        }

        public async Task<List<ProductCategoryEntity>> GetProductCategoriesAsync()
        {
            return await productCategoryCollection.Find(_ => true).ToListAsync();
        }

        public async Task<ProductCategoryEntity> GetProductCategryByIdAsync(string productCategoryId)
        {
            return await productCategoryCollection.Find(x => x.Id == productCategoryId).FirstOrDefaultAsync();
        }

        public async Task UpdateProductCategoryAsync(ProductCategoryEntity productCategory)
        {
            await productCategoryCollection.ReplaceOneAsync(x => x.Id == productCategory.Id, productCategory);
        }
    }
}

