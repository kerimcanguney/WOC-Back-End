using API.Data;
using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SeedData
{
    public static class SeedData
    {
        private static IEncryptionService _encrypt = new EncryptionService();
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AccountContext(serviceProvider.GetRequiredService<DbContextOptions<AccountContext>>()))
            {
                //Roles
                if (!context.Roles.Any())
                {
                    context.Roles.Add(new Role() { Name = "Standard" });
                    context.SaveChanges();
                    context.Roles.Add(new Role() { Name = "Admin" });
                    context.SaveChanges();
                }
                //Permissions
                if (!context.Permissions.Any())
                {
                    //Create permission to check if user is allowed to do something
                    context.Permissions.Add(new Permission() { Name = "Read" });
                    context.SaveChanges();
                    context.Permissions.Add(new Permission() { Name = "Create" });
                    context.SaveChanges();
                    context.Permissions.Add(new Permission() { Name = "Edit" });
                    context.SaveChanges();
                    context.Permissions.Add(new Permission() { Name = "Delete" });
                    context.SaveChanges();
                }
                //Add permissions to the roles
                if (!context.RolePermissions.Any())
                {
                    context.RolePermissions.Add(new RolePermission() { Role = context.Roles.Find(1), Permission = context.Permissions.Find(1) });
                    context.SaveChanges();
                }
                //Accounts
                if (!context.Accounts.Any())
                {
                    ////
                    var hashsalt = _encrypt.EncryptPassword("pw1");
                    context.Accounts.Add(new Account() { Name = "account1", Email = "email1", Password = hashsalt.Hash, StoredSalt = hashsalt.Salt, Role = context.Roles.Find(1) });
                    context.SaveChanges();
                    ////
                    var hashsalt2 = _encrypt.EncryptPassword("admin");
                    context.Accounts.Add(new Account() { Name = "admin", Email = "admin", Password = hashsalt2.Hash, StoredSalt = hashsalt2.Salt, Role = context.Roles.Find(2) });
                    context.SaveChanges();
                    ////
                    var hashsalt3 = _encrypt.EncryptPassword("pw3");
                    context.Accounts.Add(new Account() { Name = "name3", Email = "email3", Password = hashsalt3.Hash, StoredSalt = hashsalt3.Salt, Role = context.Roles.Find(1) });
                    context.SaveChanges();
                }
                //Companies
                if (!context.Companies.Any())
                {
                    context.Companies.Add(new Company() { Name = "Company1" });
                    context.SaveChanges();
                    context.Companies.Add(new Company() { Name = "Company2" });
                    context.SaveChanges();
                }
                //Workspaces
                if (!context.Workspaces.Any())
                {
                    context.Workspaces.Add(new Workspace() { Name = "Workspace1", Company = context.Companies.Find(1) });
                    context.SaveChanges();
                    context.Workspaces.Add(new Workspace() { Name = "Workspace2", Company = context.Companies.Find(1) });
                    context.SaveChanges();
                    context.Workspaces.Add(new Workspace() { Name = "CompanyWorkspace", Company = context.Companies.Find(2) });
                    context.SaveChanges();
                }
                //Add accounts to the workspaces
                if (!context.AccountWorkspaces.Any())
                {
                    //account #1 joins workspace #1 and #2
                    context.AccountWorkspaces.Add(new AccountWorkspace() { Account = context.Accounts.Find(1), Workspace = context.Workspaces.Find(1) });
                    context.SaveChanges();
                    context.AccountWorkspaces.Add(new AccountWorkspace() { Account = context.Accounts.Find(1), Workspace = context.Workspaces.Find(2) });
                    context.SaveChanges();
                    //Admin can view all, so no need to join
                    //Account #3 joins workspace #3
                    context.AccountWorkspaces.Add(new AccountWorkspace() { Account = context.Accounts.Find(3), Workspace = context.Workspaces.Find(3) });
                    context.SaveChanges();
                }
                //JoinRequests
                //To create a request if a user wants to join a workspace
                if (!context.JoinRequests.Any())
                {
                    //Create a request for account #1 to join workspace #3 if not already joined
                    if (!context.AccountWorkspaces.Any(a => a.AccountId == 1 && a.WorkspaceId == 3))
                    {
                        context.JoinRequests.Add(new JoinRequest() { Account = context.Accounts.Find(1), Workspace = context.Workspaces.Find(3) });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
