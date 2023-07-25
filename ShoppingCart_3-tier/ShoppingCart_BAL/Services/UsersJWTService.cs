using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Repositories;

namespace ShoppingCart_BAL.Services
{
    public class UsersJWTService
    {
        private UsersJWTRepository _usersRepository;

        public UsersJWTService(UsersJWTRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public bool AuthenticateUserJWT(string username, string password)
        {
            UsersJWT user = _usersRepository.GetUserByUsername(username);

            if (user != null && user.Password == password)
            {
                return true;
            }
            return false;
        }

        public UsersJWT CreatAccountJWT(UsersJWT account)
        {
            return _usersRepository.CreatNewUserAcc(account);
        }
    }
}
