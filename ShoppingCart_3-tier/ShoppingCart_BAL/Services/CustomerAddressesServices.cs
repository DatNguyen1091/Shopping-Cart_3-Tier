using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class CustomerAddressesServices
    {
        private CustomerAddressesRepository _customerAddressesRepository;

        public CustomerAddressesServices(CustomerAddressesRepository customerAddressesRepository)
        {
            _customerAddressesRepository = customerAddressesRepository;
        }

        public List<CustomerAddresses> GetAllCustomerAddresses(int? page)
        {
            return _customerAddressesRepository.GetAll(page);
        }

        public CustomerAddresses GetCustomerAddressesById(int id)
        {
            return _customerAddressesRepository.GetById(id);
        }

        public CustomerAddresses CreateCustomerAddresses(CustomerAddresses model)
        {
            return _customerAddressesRepository.Create(model);
        }

        public CustomerAddresses UpdateCustomerAddresses(int id, CustomerAddresses model)
        {
            var result = _customerAddressesRepository.Update(id, model);
            return result;
        }

        public string RemoveCustomerAddresses(int id)
        {
            var result = _customerAddressesRepository.Delete(id);
            return result;
        }
    }
}
