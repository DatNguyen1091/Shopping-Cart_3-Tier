using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class UsersCookieService
    {
        private UsersCookieRepository _usersRepository;

        public UsersCookieService(UsersCookieRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public bool AuthenticateUser(string username, string password)
        {
            UsersCookie user = _usersRepository.GetUserByUsername(username);

            if (user != null && user.Password == password)
            {
                return true; 
            }
            return false; 
        }

        public UsersCookie CreatAccount(UsersCookie account)
        {
            return _usersRepository.CreatNewUserAcc(account);
        }
    }
}
