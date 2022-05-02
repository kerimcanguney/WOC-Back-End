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
        [HttpGet("Accounts")]
        public IEnumerable<AccountAccountsViewModel> Accounts()
        {
            List<Account> accs = _service.GetAccounts();
            List<AccountAccountsViewModel> avms = new();
            for (int i = 0; i < accs.Count(); i++)
            {
                avms.Add(new(accs[i]));
            }
            return avms;
        }
        [HttpGet("Account")]
        public AccountAccountViewModel Account(int id)
        {
            Account a = _service.GetAccountById(id);
            return new(a);
        }

        [HttpPost("Login")]
        public AccountLoginViewModel Login(string email, string password)
        {
            Account a = _service.LoginAccount(email,password);
            string token = _jwt.GenerateToken(a);
            return new(a, token);
        }
        
        [HttpPost("Register")]
        public AccountRegisterViewModel Register(string name, string email, string password)
        {
            //Account a = new() { Name = name, Email = email, Password = password};
            Account a = _service.RegisterAccount(new() { Name = name, Email = email, Password = password });
            if (a != null)
            {
                string token =  _jwt.GenerateToken(a);
                return new(a, token);
            }
            else
            {
                return null; //invalid request -> no account created
            }
        }
    }
}
