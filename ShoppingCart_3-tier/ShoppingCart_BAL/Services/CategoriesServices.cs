using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class CategoriesServices
    {
        private CategoriesRepository _categoriesRepository;

        public CategoriesServices(CategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public List<Categories> GetAllCategories(int? page)
        {
            return _categoriesRepository.GetAll(page);
        }

        public Categories GetCategoriesById(int id)
        {
            return _categoriesRepository.GetById(id);
        }

        public Categories CreateCategories(Categories model)
        {
            return _categoriesRepository.Create(model);
        }

        public Categories UpdateCategories(int id, Categories model)
        {
            var result = _categoriesRepository.Update(id, model);
            return result;
        }

        public string RemoveCategories(int id)
        {
            var result = _categoriesRepository.Delete(id);
            return result;
        }
    }
}
