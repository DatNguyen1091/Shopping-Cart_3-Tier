using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly CustomerAddressesServices _customerAddressesServices;

        public CustomerAddressesController(CustomerAddressesServices customerAddressesServices)
        {
            _customerAddressesServices = customerAddressesServices;
        }

        [HttpGet]
        public List<CustomerAddresses> GetCustomerAddresses(int? page)
        {
            return _customerAddressesServices.GetAllCustomerAddresses(page);
        }

        [HttpGet("{id}")]
        public CustomerAddresses GetCustomerAddressesId(int id)
        {
            return _customerAddressesServices.GetCustomerAddressesById(id);
        }

        [HttpPost]
        public CustomerAddresses PostCustomerAddresses(CustomerAddresses model)
        {
            return _customerAddressesServices.CreateCustomerAddresses(model);
        }

        [HttpPut("{id}")]
        public CustomerAddresses PutCustomerAddresses(int id, CustomerAddresses model)
        {
            return _customerAddressesServices.UpdateCustomerAddresses(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteCustomerAddresses(int id)
        {
            return _customerAddressesServices.RemoveCustomerAddresses(id);
        }
    }
}
