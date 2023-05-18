using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public List<Products> GetProducts(int? page)
        {
            return _productsService.GetAllProducts(page);
        }

        [HttpGet("{id}")]
        public Products GetProductsId(int id)
        {
            return _productsService.GetProductsById(id);
        }

        [HttpPost]
        public Products PostProducts(Products model)
        {
            return _productsService.CreateProducts(model);
        }

        [HttpPut("{id}")]
        public Products PutProducts(int id, Products model)
        {
            return _productsService.UpdateProducts(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteProducts(int id)
        {
            return _productsService.RemoveProducts(id);
        }
    }
}
