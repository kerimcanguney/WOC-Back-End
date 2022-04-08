using API.Data;
using API.Models;
using API.Services;
using API.ViewModels;
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
        public IEnumerable<AccountViewModel> Get()
        {
            List<Account> accs = _service.GetAccounts();
            List<AccountViewModel> avms = new();
            for (int i = 0; i < accs.Count(); i++)
            {
                avms.Add(new(accs[i]));
            }
            return avms;
        }
        [HttpPost("Login")]
        public AccountViewModel Login(string email, string password)
        {
            Account a = _service.LoginAccount(email,password);
            string token = "token"; //Generate token
            token = Generate(a)
            
            return new(a,token);
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
        public AccountViewModel Register(string name, string email, string password)
        {
            //Account acc = new() { Name = "test1", Email = "email1", Password = "password1" };
            //_service.RegisterAccount(acc);
            Account a = new() { Name = name, Email = email, Password = password};
            bool registered = _service.RegisterAccount(a);
            if (registered)
            {
                string token = "token"; //Generate token
                token = Generate(new Account() { Name = name, Email = email, Password = password });
                return new(a, token);
            }
            else
            {
                return null; //invalid request -> no account created
            }
        }
    }
}
