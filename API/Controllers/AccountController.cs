using API.Data;
using API.Models;
using API.Services;
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
        public IEnumerable<Account> Get()
        {
            //Account acc = new() { Name = "test1", Email = "email1", Password = "password1", Role = new() {Name = "role1" } };
            //_service.RegisterAccount(acc);
            return _service.GetAccounts();
        }
        [HttpPost]
        public Account Login(string email, string password)
        {
            return _service.LoginAccount(email,password);
        }
    }
}
