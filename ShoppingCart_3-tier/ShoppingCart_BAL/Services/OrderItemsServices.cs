using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class OrderItemsServices
    {
        private OrderItemsRepository _orderItemsRepository;

        public OrderItemsServices(OrderItemsRepository orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        public List<OrderItems> GetAllOrderItems(int? page)
        {
            return _orderItemsRepository.GetAll(page);
        }

        public OrderItems GetOrderItemsById(int id)
        {
            return _orderItemsRepository.GetById(id);
        }

        public OrderItems CreateOrderItems(OrderItems model)
        {
            return _orderItemsRepository.Create(model);
        }

        public OrderItems UpdateOrderItems(int id, OrderItems model)
        {
            var result = _orderItemsRepository.Update(id, model);
            return result;
        }

        public string RemoveOrderItems(int id)
        {
            var result = _orderItemsRepository.Delete(id);
            return result;
        }
    }
}
