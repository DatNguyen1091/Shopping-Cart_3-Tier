using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersServices _ordersServices;

        public OrdersController(OrdersServices ordersServices)
        {
            _ordersServices = ordersServices;
        }

        [HttpGet]
        public List<Order> GetOrder(int? page)
        {
            return _ordersServices.GetAllOrder(page);
        }

        [HttpGet("{id}")]
        public Order GetOrderId(int id)
        {
            return _ordersServices.GetOrderById(id);
        }

        [HttpPost]
        public Order PostOrder(Order model)
        {
            return _ordersServices.CreateOrder(model);
        }

        [HttpPut("{id}")]
        public Order PutOrder(int id, Order model)
        {
            return _ordersServices.UpdateOrder(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteOrder(int id)
        {
            return _ordersServices.RemoveOrder(id);
        }
    }
}
