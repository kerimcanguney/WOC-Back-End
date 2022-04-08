using API.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private IConfiguration _config;

        public AccountController(IAccountContext context, IConfiguration config)
        {
            _service = new AccountService(context);
            _config = config; ;
        }
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            //Account acc = new() { Name = "test1", Email = "email1", Password = "password1", Role = new() {Name = "role1" } };
            //_service.RegisterAccount(acc);
            return _service.GetAccounts();
        }

        [HttpPost("Login")]
        public string Login(string email, string password)
        {
            Account loginAccount = _service.LoginAccount(email, password);

            return Generate(loginAccount);
        }
        private string Generate(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Name),
                new Claim(ClaimTypes.Email, account.Email),
                //new Claim(ClaimTypes.Role, account.Role.Name) 
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private Account GetCurrentUserViaHttpContext()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null) return null;

            var userClaims = identity.Claims;

            return new Account
            {
                Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                Role = new Role() { Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value }

            };
        }
        [HttpPost("Register")]
        public string Register(string username, string email, string password)
        {
            return Generate(new Account() { Name = username, Email = email, Password = password });
        }
    }
}
