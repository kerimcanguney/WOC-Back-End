﻿using API.Data;
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

        public List<JoinRequest> GetJoinRequests()
        {
            return _context.JoinRequests
                .Include(jr => jr.Account)
                .Include(jr => jr.Workspace)
                .ToList();
        }
        public bool Join(int userId, int workspaceId)
        {
            try
            {
                //Check if exists (acc/ws) //Check if account is already in workspace
                _context.JoinRequests.Add(new() { Account = _context.Accounts.Find(userId), IsAccepted = false, Workspace = _context.Workspaces.Find(workspaceId) });
                _context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Could not create joinrequest");
            }
        }
        public bool AcceptJoinRequest(int userId, int workspaceId)
        {
            JoinRequest joinRequest = _context.JoinRequests
                .Single(jr => jr.AccountId == userId && jr.WorkspaceId == workspaceId);
            joinRequest.IsAccepted = true;
            _context.JoinRequests
                .Update(joinRequest);
            _context.AccountWorkspaces.Add(new() { Account = _context.Accounts.Find(userId), Workspace = _context.Workspaces.Find(workspaceId) });
            _context.SaveChanges();
            return true;
        }
    }
}
