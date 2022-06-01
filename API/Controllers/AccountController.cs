using API.Data;
using API.Models;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IJWTService _jwt;

        public AccountController(IAccountContext context, IConfiguration config)
        {
            _service = new AccountService(context);
            _jwt = new JWTService(config);
        }
        [HttpGet]
        public IEnumerable<AccountAccountsViewModel> Get()
        {
            List<Account> accs = _service.GetAccounts();
            List<AccountAccountsViewModel> avms = new();
            for (int i = 0; i < accs.Count(); i++)
            {
                avms.Add(new(accs[i]));
            }
            return avms;
        }
        [HttpGet("{id}")]
        public AccountAccountViewModel Account(int id)
        {
            Account a = _service.GetAccountById(id);
            return new(a);
        }
        [HttpGet("Info")]
        public AccountInfoViewModel Info(string token)
        {
            if (!_jwt.ValidateToken(token))
            {
                throw new InvalidOperationException("Invalid token");
            }
            string email = _jwt.GetClaim(token, "Email");
            Account a = _service.GetAccount(email);
            string name = a.Name;
            string role = a.Role.Name;
            int id = a.Id;
            return new(id, name, email, role);
        }

        [HttpPost("Login")]
        public TokenViewModel Login(string email, string password)
        {
            if (!_service.LoginAccount(email, password))
            {
                throw new InvalidOperationException("Invalid info");
            }
            Account a = _service.GetAccount(email);
            string token = _jwt.GenerateToken(a); //Generate token
            return new(token);
        }
        
        [HttpPost("Register")]
        public TokenViewModel Register(string name, string email, string password)
        {
            bool registered = _service.RegisterAccount(name, email, password);
            if (registered)
            {
                Account a = _service.GetAccount(email);
                string token = _jwt.GenerateToken(a); //Generate token
                return new(token);
            }
            else
            {
                throw new InvalidOperationException("Invalid info");
            }
        }
    }
}
