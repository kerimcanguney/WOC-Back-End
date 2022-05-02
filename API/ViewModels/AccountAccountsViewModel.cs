using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class AccountAccountsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleViewModel Role { get; set; }
        public List<WorkspaceViewModel> Workspaces { get; set; }
        public AccountAccountsViewModel(Account account)
        {
            Id = account.Id;
            Name = account.Name;
            Email = account.Email;
            Password = account.Password;
            if (account.Role != null)
            {
                Role = new(account.Role);
            }
            Workspaces = new();
            if (account.Workspaces != null)
            {
                for (int i = 0; i < account.Workspaces.Count; i++)
                {
                    Workspaces.Add(new(account.Workspaces[i].Workspace));
                }
            }
        }
    }
}
