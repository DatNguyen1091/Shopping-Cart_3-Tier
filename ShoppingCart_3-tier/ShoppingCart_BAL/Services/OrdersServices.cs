using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class OrdersServices
    {
        private OrderRepository _orderRepository;

        public OrdersServices(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrder(int? page)
        {
            return _orderRepository.GetAll(page);
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public Order CreateOrder(Order model)
        {
            return _orderRepository.Create(model);
        }

        public Order UpdateOrder(int id, Order model)
        {
            var result = _orderRepository.Update(id, model);
            return result;
        }

        public string RemoveOrder(int id)
        {
            var result = _orderRepository.Delete(id);
            return result;
        }
    }
}
