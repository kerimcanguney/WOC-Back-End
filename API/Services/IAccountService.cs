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
        public bool InsertAccount(Account account);
        public bool UpdateAccount(Account account);
        public bool DeleteAccount(Account account);
        public bool RegisterAccount(Account account);
        public Account LoginAccount(string email, string password);
    }
}
