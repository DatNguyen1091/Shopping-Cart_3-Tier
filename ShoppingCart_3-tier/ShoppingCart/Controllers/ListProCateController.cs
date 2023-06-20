using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListProCateController : ControllerBase
    {
        private readonly ListProCateServices _listProCateServices;

        public ListProCateController(ListProCateServices listProCateServices)
        {
            _listProCateServices = listProCateServices;
        }

        [HttpGet]
        public List<ListProCate> GetListProCate(int? page)
        {
            return _listProCateServices.GetAllListProCate(page);
        }
    }
}
