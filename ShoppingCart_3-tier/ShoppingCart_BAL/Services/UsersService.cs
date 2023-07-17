using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class UsersService
    {
        private UsersRepository _usersRepository;

        public UsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public bool AuthenticateUser(string username, string password)
        {
            Users user = _usersRepository.GetUserByUsername(username);

            if (user != null && user.Password == password)
            {
                return true; 
            }
            return false; 
        }
    }
}
