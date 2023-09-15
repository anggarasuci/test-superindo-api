using ProductApi.Data.Entity;

namespace ProductApi.Data.Service
{
    public interface IProductCategoryService
    {
        public Task<List<ProductCategoryEntity>> GetProductCategoriesAsync();
        public Task<ProductCategoryEntity> GetProductCategryByIdAsync(string productCategoryId);
        public Task AddProductCategoryAsync(ProductCategoryEntity productCategory);
        public Task UpdateProductCategoryAsync(ProductCategoryEntity productCategory);
        public Task DeleteProductCategoryAsync(string productCategoryId);
    }
}

