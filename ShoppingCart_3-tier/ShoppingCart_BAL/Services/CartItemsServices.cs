using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class CartItemsServices
    {
        private CartItemsRepository _cartItemsRepository;
        public CartItemsServices(CartItemsRepository cartItemsRepository)
        {
            _cartItemsRepository = cartItemsRepository;
        }

        public List<CartItems> GetAllCartItems(int? page)
        {
            return _cartItemsRepository.GetAll(page);
        }

        public CartItems GetCartItemsById(int id)
        {
            return _cartItemsRepository.GetById(id);
        }

        public CartItems CreateCartItems(CartItems model)
        {
            return _cartItemsRepository.Create(model);
        }

        public CartItems UpdateCartItems(int id, CartItems model)
        {
            var result = _cartItemsRepository.Update(id, model);
            return result;
        }

        public string RemoveCartItems(int id)
        {
            var result = _cartItemsRepository.Delete(id);
            return result;
        }
    }
}
