using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BrandsServices _brandsServices;

        public BrandsController(BrandsServices brandsServices)
        {
            _brandsServices = brandsServices;
        }

        [HttpGet]
        public List<Brands> GetBrands(int? page)
        {
            return _brandsServices.GetAllBrands(page);
        }

        [HttpGet("{id}")]
        public Brands GetBrandsId(int id)
        {
            return _brandsServices.GetBrandsById(id);
        }

        [HttpPost]
        public Brands PostBrands(Brands model)
        {
            return _brandsServices.CreateBrands(model);
        }

        [HttpPut("{id}")]
        public Brands PutBrands(int id, Brands model)
        {
            return _brandsServices.UpdateBrands(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteBrands(int id)
        {
            return _brandsServices.RemoveBrands(id);
        }
    }
}
