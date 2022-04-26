using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class AccountService : IAccountService , IWorkspaceService
    {
        private readonly IAccountContext _context;
        public AccountService(IAccountContext context)
        {
            _context = context;
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
        //Check if email exists
        private bool DoesEmailExist(string email)
        {
             return _context.Accounts.Any(a => a.Email == email);
        }
        //Get Accounts (All)
        public List<Account> GetAccounts()
        {
            return _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.Workspaces)
                .ThenInclude(a => a.Workspace)
                .ThenInclude(a => a.Company)
                .ToList();
        }
        //Get Account (id)
        public Account GetAccountById(int id)
        {
            return _context.Accounts
                .Include(a => a.Role)
                .Include(a => a.Workspaces)
                .ThenInclude(a => a.Workspace)
                .ThenInclude(a => a.Company)
                .SingleOrDefault(a => a.Id == id);
        }
        //Register
        public Account RegisterAccount(Account account)
        {
            if (!DoesEmailExist(account.Email))
            {
                account.Role = _context.Roles.Find(1); //set standard role
                if (InsertAccount(account))
                {
                    return GetAccountById(account.Id);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public Account LoginAccount(string email, string password)
        {
            Account acc;
            try
            {
                acc = _context.Accounts.Where(a => a.Email == email)
                .Include(a => a.Role)
                .Include(a => a.Workspaces)
                .ThenInclude(a => a.Workspace)
                .Single();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Email not found");
            }
            if (acc.Password == password) //Check password
            {
                return acc;
            }
            else
            {
                throw new InvalidOperationException("Password incorrect");
            }
        }

        public List<Workspace> GetWorkspaces()
        {
            return _context.Workspaces
                .Include(w => w.Company)
                .Include(w => w.Accounts)
                .ThenInclude(w => w.Account)
                .ToList();
        }
    }
}
