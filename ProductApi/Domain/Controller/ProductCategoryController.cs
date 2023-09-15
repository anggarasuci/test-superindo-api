using Microsoft.AspNetCore.Mvc;
using ProductApi.Data.Entity;
using ProductApi.Data.Service;

namespace ProductApi.Domain.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            this.productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<List<ProductCategoryEntity>> Get()
        {
            return await productCategoryService.GetProductCategoriesAsync();
        }

        [HttpGet("{productCategoryId:length(24)}")]
        public async Task<ActionResult<ProductCategoryEntity>> Get(string productCategoryId)
        {
            var productDetails = await productCategoryService.GetProductCategryByIdAsync(productCategoryId);
            if (productDetails is null)
            {
                return NotFound();
            }
            return productDetails;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCategoryEntity productCategoryEntity)
        {
            await productCategoryService.AddProductCategoryAsync(productCategoryEntity);
            return CreatedAtAction(nameof(Get), new
            {
                id = productCategoryEntity.Id
            }, productCategoryEntity);
        }

        [HttpDelete("{productCategoryId:length(24)}")]
        public async Task<IActionResult> Delete(string productCategoryId)
        {
            var result = await productCategoryService.GetProductCategryByIdAsync(productCategoryId);
            if (result is null)
            {
                return NotFound();
            }
            await productCategoryService.DeleteProductCategoryAsync(productCategoryId);
            return Ok();
        }
    }
}

