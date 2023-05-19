using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly CartsServices _cartsServices;

        public CartsController(CartsServices cartsServices)
        {
            _cartsServices = cartsServices;
        }

        [HttpGet]
        public List<Carts> GetCarts(int? page)
        {
            return _cartsServices.GetAllCarts(page);
        }

        [HttpGet("{id}")]
        public Carts GetCartsId(int id)
        {
            return _cartsServices.GetCartsById(id);
        }

        [HttpPost]
        public Carts PostCarts(Carts model)
        {
            return _cartsServices.CreateCarts(model);
        }

        [HttpPut("{id}")]
        public Carts PutCarts(int id, Carts model)
        {
            return _cartsServices.UpdateCarts(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteCarts(int id)
        {
            return _cartsServices.RemoveCarts(id);
        }
    }
}
