using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ProductCategoriesServices _productCategoriesServices;

        public ProductCategoriesController(ProductCategoriesServices productCategoriesServices)
        {
            _productCategoriesServices = productCategoriesServices;
        }

        [HttpGet]
        public List<ProductCategories> GetProductCategories(int? page)
        {
            return _productCategoriesServices.GetAllProductCategories(page);
        }

        [HttpGet("{id}")]
        public ProductCategories GetProductCategoriesId(int id)
        {
            return _productCategoriesServices.GetProductCategoriesById(id);
        }

        [HttpPost]
        public ProductCategories PostProductCategories(ProductCategories model)
        {
            return _productCategoriesServices.CreateProductCategories(model);
        }

        [HttpPut("{id}")]
        public ProductCategories PutProductCategories(int id, ProductCategories model)
        {
            return _productCategoriesServices.UpdateProductCategories(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteProductCategories(int id)
        {
            return _productCategoriesServices.RemoveProductCategories(id);
        }
    }
}
