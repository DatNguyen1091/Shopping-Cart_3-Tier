using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderItemsServices _orderItemsServices;

        public OrderItemsController(OrderItemsServices orderItemsServices)
        {
            _orderItemsServices = orderItemsServices;
        }

        [HttpGet]
        public List<OrderItems> GetOrderItems(int? page)
        {
            return _orderItemsServices.GetAllOrderItems(page);
        }

        [HttpGet("{id}")]
        public OrderItems GetOrderItemsId(int id)
        {
            return _orderItemsServices.GetOrderItemsById(id);
        }

        [HttpPost]
        public OrderItems PostOrderItems(OrderItems model)
        {
            return _orderItemsServices.CreateOrderItems(model);
        }

        [HttpPut("{id}")]
        public OrderItems PutOrderItems(int id, OrderItems model)
        {
            return _orderItemsServices.UpdateOrderItems(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteOrderItems(int id)
        {
            return _orderItemsServices.RemoveOrderItems(id);
        }
    }
}
