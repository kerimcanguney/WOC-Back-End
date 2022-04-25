using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class AccountContext : DbContext, IAccountContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountWorkspace>().HasKey(aw => new { aw.AccountId, aw.WorkspaceId });
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<JoinRequest>().HasKey(jr => new { jr.AccountId, jr.WorkspaceId });
        }
        public AccountContext(DbContextOptions<AccountContext> options)
        : base(options)
        {
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=PIMAccounts;Trusted_Connection=True;");
        }*/
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountWorkspace> AccountWorkspaces { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }
    }
}
