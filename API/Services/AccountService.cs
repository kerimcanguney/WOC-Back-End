using API.Data;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountContext _context;
        public AccountService(IAccountContext context)
        {
            _context = context;
        }
        public List<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }
        public Account GetAccountById(int id)
        {
            return _context.Accounts.Find(id);
        }
        public bool InsertAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            return true;
        }
        public bool DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            return true;
        }
        //Check if exists
        private bool DoesEmailExist(string email)
        {
             return _context.Accounts.Any(a => a.Email == email);
        }   

        //Register
        public bool RegisterAccount(Account account)
        {
            if (!DoesEmailExist(account.Email))
            {
                return InsertAccount(account);
            }
            else
            {
                return false;
            }
        }
        public Account LoginAccount(string email, string password)
        {
            Account acc =_context.Accounts.Where(a => a.Email == email).Single();
            if (acc.Password == password)
            {
                return acc;
            }
            else
            {
                return null;
            }
        }
    }
}
