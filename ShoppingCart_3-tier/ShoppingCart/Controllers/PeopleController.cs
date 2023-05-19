using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleServices _peopleServices;

        public PeopleController(PeopleServices peopleServices)
        {
            _peopleServices = peopleServices;
        }

        [HttpGet]
        public List<People> GetPeople(int? page)
        {
            return _peopleServices.GetAllPeople(page);
        }

        [HttpGet("{id}")]
        public People GetPeopleId(int id)
        {
            return _peopleServices.GetPeopleById(id);
        }

        [HttpPost]
        public People PostPeople(People model)
        {
            return _peopleServices.CreatePeople(model);
        }

        [HttpPut("{id}")]
        public People PutPeople(int id, People model)
        {
            return _peopleServices.UpdatePeople(id, model);
        }

        [HttpDelete("{id}")]
        public string DeleteAPeople(int id)
        {
            return _peopleServices.RemovePeople(id);
        }
    }
}
