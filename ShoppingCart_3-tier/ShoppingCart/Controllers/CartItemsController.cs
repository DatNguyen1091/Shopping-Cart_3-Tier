using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly CartItemsServices _cartItemsServices;

        public CartItemsController(CartItemsServices cartItemsServices)
        {
            _cartItemsServices = cartItemsServices;
        }

        [HttpGet]
        public List<CartItems> GetCartItems(int? page)
        {
            return _cartItemsServices.GetAllCartItems(page);
        }

        [HttpGet("{id}")]
        public CartItems GetCartItemsId(int id)
        {
            return _cartItemsServices.GetCartItemsById(id);
        }

        [HttpPost]
        public CartItems PostCartItems(CartItems model)
        {
            return _cartItemsServices.CreateCartItems(model);
        }

        [HttpPut("{id}")]
        public CartItems PutCartItems(int id, CartItems model)
        {
            return _cartItemsServices.UpdateCartItems(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteCartItems(int id)
        {
            return _cartItemsServices.RemoveCartItems(id);
        }
    }
}
