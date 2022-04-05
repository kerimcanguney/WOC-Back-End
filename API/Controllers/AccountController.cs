using API.Data;
using API.Models;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
        public AccountController(IAccountContext context)
        {
            _service = new AccountService(context);
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
            return new(a,token);
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
                return new(a, token);
            }
            else
            {
                return null; //invalid request -> no account created
            }
        }
    }
}
