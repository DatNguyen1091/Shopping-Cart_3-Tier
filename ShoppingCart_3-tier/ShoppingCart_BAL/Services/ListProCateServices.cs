using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class ListProCateServices
    {
        private ListProCateReposiory _listProCateReposiory;

        public ListProCateServices(ListProCateReposiory listProCateReposiory)
        {
            _listProCateReposiory = listProCateReposiory;
        }

        public List<ListProCate> GetAllListProCate(int? page)
        {
            return _listProCateReposiory.GetAllInnerJoin(page);
        }

        public List<ListProCate> GetAllListProCateLJ(int? page)
        {
            return _listProCateReposiory.GetAllLeftJoin(page);
        }

        public List<ListProCate> GetAllListProCateRJ(int? page)
        {
            return _listProCateReposiory.GetAllRightJoin(page);
        }
    }
}
