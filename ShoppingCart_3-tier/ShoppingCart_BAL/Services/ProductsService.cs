using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class ProductsService
    {
        private ProductsRepository _productsRepository;
        public ProductsService(ProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public List<Products> GetAllProducts(int? page)
        {
            return _productsRepository.GetAll(page);
        }

        public Products GetProductsById(int id)
        {
            return _productsRepository.GetById(id);
        }

        public Products CreateProducts(Products model)
        {
            return _productsRepository.Create(model);
        }

        public Products UpdateProducts(int id, Products model)
        {
            var result = _productsRepository.Update(id, model);
            return result;
        }

        public string RemoveProducts(int id)
        {
            var result = _productsRepository.Delete(id); 
            return result;
        }
    }
}
