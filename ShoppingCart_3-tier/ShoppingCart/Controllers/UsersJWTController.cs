using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoppingCart_BAL.Services;
using ShoppingCart_DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersJWTController : ControllerBase
    {
        private readonly UsersJWTService _usersService;
        private readonly IConfiguration _configuration;
        public UsersJWTController(UsersJWTService usersService, IConfiguration configuration)
        {
            _usersService = usersService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public string Login(UsersJWT user)
        {
            var username = user.Username!;
            var password = user.Password!;
            bool isAuthenticated = _usersService.AuthenticateUserJWT(username, password);

            if (isAuthenticated)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(30),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey,
                        SecurityAlgorithms.HmacSha256Signature)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return "Login failed";
        }

        [HttpPost("Logout")]
        public async void Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

            Response.StatusCode = (int)HttpStatusCode.OK;
        }

        [HttpPost("Signup")]
        public UsersJWT PostAccount(UsersJWT account)
        {
            return _usersService.CreatAccountJWT(account);
        }
    }
}
