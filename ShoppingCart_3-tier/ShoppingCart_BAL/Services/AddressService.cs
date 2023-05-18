using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class AddressesService 
    {
        private AddressesRepository _addressesRepository;

        public AddressesService(AddressesRepository addressesRepository)
        {
            _addressesRepository = addressesRepository;
        }

        public List<Addresses> GetAllAddresses(int? page)
        {
            return _addressesRepository.GetAll(page);
        }

        public Addresses GetAddressById(int id)
        {
            return _addressesRepository.GetById(id);
        }

        public Addresses CreateAddress(Addresses model)
        {
            return _addressesRepository.Create(model);
        }

        public Addresses UpdateAddress(int id, Addresses model)
        {
            var result = _addressesRepository.Update(id, model);
            return result;
        }

        public string RemoveAddress(int id)
        {
            var result = _addressesRepository.Delete(id);
            return result;
        }
    }
}
