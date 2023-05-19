using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersServices _customersServices;

        public CustomersController(CustomersServices customersServices)
        {
            _customersServices = customersServices;
        }

        [HttpGet]
        public List<Customers> GetCustomers(int? page)
        {
            return _customersServices.GetAllCustomers(page);
        }

        [HttpGet("{id}")]
        public Customers GetCustomersId(int id)
        {
            return _customersServices.GetCustomersById(id);
        }

        [HttpPost]
        public Customers PostCustomers(Customers model)
        {
            return _customersServices.CreateCustomers(model);
        }

        [HttpPut("{id}")]
        public Customers PutCustomers(int id, Customers model)
        {
            return _customersServices.UpdateCustomers(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteCustomers(int id)
        {
            return _customersServices.RemoveCustomers(id);
        }
    }
}
