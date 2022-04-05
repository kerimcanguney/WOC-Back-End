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
            Account acc = new() { Name = "test1", Email = "email1", Password = "password1", Role = new() {Name = "role1" } };
            _service.RegisterAccount(acc);
            List<Account> accs = _service.GetAccounts();
            List<AccountViewModel> avms = new();
            for (int i = 0; i < accs.Count(); i++)
            {
                avms.Add(new(accs[i]));
            }
            return avms;
        }
        [HttpPost]
        public AccountViewModel Login(string email, string password)
        {
            Account a = _service.LoginAccount(email,password);
            string token = "token"; //Generate token
            return new(a,token);
        }
    }
}
