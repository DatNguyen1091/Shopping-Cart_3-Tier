using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class CartsServices
    {
        private CartsRepository _cartsRepository;

        public CartsServices(CartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public List<Carts> GetAllCarts(int? page)
        {
            return _cartsRepository.GetAll(page);
        }

        public Carts GetCartsById(int id)
        {
            return _cartsRepository.GetById(id);
        }

        public Carts CreateCarts(Carts model)
        {
            return _cartsRepository.Create(model);
        }

        public Carts UpdateCarts(int id, Carts model)
        {
            var result = _cartsRepository.Update(id, model);
            return result;
        }

        public string RemoveCarts(int id)
        {
            var result = _cartsRepository.Delete(id);
            return result;
        }
    }
}
