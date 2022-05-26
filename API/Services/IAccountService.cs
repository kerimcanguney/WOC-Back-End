using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IAccountService
    {
        public List<Account> GetAccounts();
        public Account GetAccountById(int id);
        public Account GetAccount(string email);
        public bool RegisterAccount(string name, string email, string password);
        public bool LoginAccount(string email, string password);
    }
}
