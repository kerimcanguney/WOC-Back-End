using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public string Login(UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return token;
            }

            return "No token generated";
        }

        private string Generate(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Name),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role.Name) // Role LVL ipv name
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Account Authenticate(UserLogin userLogin)
        {
            //INMEMORY USER
            Models.Account NOOB = new Models.Account() { Name = "noob", Password = "noob", Email = "noob@noob.com", Role = new Models.Role() { Name = "noob" } };
            Models.Account ADMIN = new Models.Account() { Name = "admin", Password = "admin", Email = "admin@admin.com", Role = new Models.Role() { Name = "Admin" } };
            //
            // IF USERINDB() return ACCOUNT MODEL


            var currentUser = userLogin.Email == NOOB.Email && userLogin.Password == NOOB.Password;

            if (currentUser == true)
            {
                return NOOB;
            }
            else if (userLogin.Email == ADMIN.Email)
            {
                return ADMIN;
            }

            return null;
        }
    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
