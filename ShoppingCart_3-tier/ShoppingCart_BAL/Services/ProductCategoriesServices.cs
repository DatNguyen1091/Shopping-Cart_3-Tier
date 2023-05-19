using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class ProductCategoriesServices
    {
        private ProductCategoriesRepository _productCategoriesRepository;

        public ProductCategoriesServices(ProductCategoriesRepository productCategoriesRepository)
        {
            _productCategoriesRepository = productCategoriesRepository;
        }

        public List<ProductCategories> GetAllProductCategories(int? page)
        {
            return _productCategoriesRepository.GetAll(page);
        }

        public ProductCategories GetProductCategoriesById(int id)
        {
            return _productCategoriesRepository.GetById(id);
        }

        public ProductCategories CreateProductCategories(ProductCategories model)
        {
            return _productCategoriesRepository.Create(model);
        }

        public ProductCategories UpdateProductCategories(int id, ProductCategories model)
        {
            var result = _productCategoriesRepository.Update(id, model);
            return result;
        }

        public string RemoveProductCategories(int id)
        {
            var result = _productCategoriesRepository.Delete(id);
            return result;
        }
    }
}
