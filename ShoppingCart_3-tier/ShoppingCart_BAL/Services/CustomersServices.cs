using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class CustomersServices
    {
        private CustomersRepository _customersRepository;

        public CustomersServices(CustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public List<Customers> GetAllCustomers(int? page)
        {
            return _customersRepository.GetAll(page);
        }

        public Customers GetCustomersById(int id)
        {
            return _customersRepository.GetById(id);
        }

        public Customers CreateCustomers(Customers model)
        {
            return _customersRepository.Create(model);
        }

        public Customers UpdateCustomers(int id, Customers model)
        {
            var result = _customersRepository.Update(id, model);
            return result;
        }

        public string RemoveCustomers(int id)
        {
            var result = _customersRepository.Delete(id);
            return result;
        }
    }
}
