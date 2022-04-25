using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public interface IAccountContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountWorkspace> AccountWorkspaces { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }
        int SaveChanges();
    }
}
