using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBrandsController : ControllerBase
    {
        private readonly ProductBrandsServices _productBrandsServices;

        public ProductBrandsController(ProductBrandsServices productBrandsServices)
        {
            _productBrandsServices = productBrandsServices;
        }

        [HttpGet]
        public List<ProductBrands> GetProductBrands(int? page)
        {
            return _productBrandsServices.GetAllProductBrands(page);
        }

        [HttpGet("{id}")]
        public ProductBrands GetProductBrandsId(int id)
        {
            return _productBrandsServices.GetProductBrandsById(id);
        }

        [HttpPost]
        public ProductBrands PostProductBrands(ProductBrands model)
        {
            return _productBrandsServices.CreateProductBrands(model);
        }

        [HttpPut("{id}")]
        public ProductBrands PutProductBrands(int id, ProductBrands model)
        {
            return _productBrandsServices.UpdateProductBrands(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteProductBrands(int id)
        {
            return _productBrandsServices.RemoveProductBrands(id);
        }
    }
}
