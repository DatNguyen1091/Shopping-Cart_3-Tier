using Microsoft.AspNetCore.Mvc;
using ShoppingCart_DAL.Models;
using ShoppingCart_BAL.Services;
using Microsoft.Extensions.Options;
using ShoppingCart_DAL.Data;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly AddressesService _addressesService;

        public AddressesController(IOptions<Connection> connection)
        {
            _addressesService = new AddressesService(connection);
        }

        [HttpGet]
        public List<Addresses> GetAddresses(int? page)
        {
            return _addressesService.GetAllAddresses(page);
        }

        [HttpGet("{id}")]
        public Addresses GetAddressId(int id)
        {
            return _addressesService.GetAddressById(id);
        }

        [HttpPost]
        public Addresses PostAddress(Addresses model)
        {
            return _addressesService.CreateAddress(model);
        }

        [HttpPut("{id}")]
        public Addresses PutAddress(int id, Addresses model)
        {
            return _addressesService.UpdateAddress(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteAddress(int id)
        {
            return _addressesService.RemoveAddress(id);
        }
    }
}
