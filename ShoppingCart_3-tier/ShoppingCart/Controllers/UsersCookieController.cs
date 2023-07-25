using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart_BAL.Services;
using System.Security.Claims;
using System.Net;
using ShoppingCart_DAL.Models;
using ShoppingCart_DAL.Data;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersCookieController : ControllerBase
    {
        private readonly UsersCookieService _usersService;

        public UsersCookieController(UsersCookieService usersService)
        {
            _usersService = usersService;
        }
        
        [HttpPost("Login")]
        public async void Login(UsersCookie user)
        {   
            var username = user.Username!;
            var password = Md5Password.MD5Hash(user.Password!);

            bool isAuthenticated = _usersService.AuthenticateUser(username, password);

            if (isAuthenticated)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
        }


        [HttpPost("Logout")]
        public async void Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.StatusCode = (int)HttpStatusCode.OK;
        }

        [HttpPost("Signup")]
        public UsersCookie PostAccount(UsersCookie account)
        {
            account.Password = Md5Password.MD5Hash(account.Password!);
            return _usersService.CreatAccount(account);
        }
    }
}
