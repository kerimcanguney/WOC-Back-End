using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleViewModel Role { get; set; }
        public AccountViewModel(Account account)
        {
            Id = account.Id;
            Name = account.Name;
            Email = account.Email;
            if (account.Role != null)
            {
                Role = new(account.Role);
            }
        }
    }
}
