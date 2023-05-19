using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class ProductBrandsServices
    {
        private ProductBandsRepository _productBandsRepository;

        public ProductBrandsServices(ProductBandsRepository productBandsRepository)
        {
            _productBandsRepository = productBandsRepository;
        }

        public List<ProductBrands> GetAllProductBrands(int? page)
        {
            return _productBandsRepository.GetAll(page);
        }

        public ProductBrands GetProductBrandsById(int id)
        {
            return _productBandsRepository.GetById(id);
        }

        public ProductBrands CreateProductBrands(ProductBrands model)
        {
            return _productBandsRepository.Create(model);
        }

        public ProductBrands UpdateProductBrands(int id, ProductBrands model)
        {
            var result = _productBandsRepository.Update(id, model);
            return result;
        }

        public string RemoveProductBrands(int id)
        {
            var result = _productBandsRepository.Delete(id);
            return result;
        }
    }
}
