using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class PeopleServices
    {
        private PeopleRepository _peopleRepository;

        public PeopleServices(PeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        public List<People> GetAllPeople(int? page)
        {
            return _peopleRepository.GetAll(page);
        }

        public People GetPeopleById(int id)
        {
            return _peopleRepository.GetById(id);
        }

        public People CreatePeople(People model)
        {
            return _peopleRepository.Create(model);
        }

        public People UpdatePeople(int id, People model)
        {
            var result = _peopleRepository.Update(id, model);
            return result;
        }

        public string RemovePeople(int id)
        {
            var result = _peopleRepository.Delete(id);
            return result;
        }
    }
}
