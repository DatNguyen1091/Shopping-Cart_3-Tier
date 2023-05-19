using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesServices _categoriesServices;

        public CategoriesController(CategoriesServices categoriesServices)
        {
            _categoriesServices = categoriesServices;
        }

        [HttpGet]
        public List<Categories> GetCategories(int? page)
        {
            return _categoriesServices.GetAllCategories(page);
        }

        [HttpGet("{id}")]
        public Categories GetCategoriesId(int id)
        {
            return _categoriesServices.GetCategoriesById(id);
        }

        [HttpPost]
        public Categories PostCategories(Categories model)
        {
            return _categoriesServices.CreateCategories(model);
        }

        [HttpPut("{id}")]
        public Categories PutCategories(int id, Categories model)
        {
            return _categoriesServices.UpdateCategories(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteCategories(int id)
        {
            return _categoriesServices.RemoveCategories(id);
        }
    }
}
