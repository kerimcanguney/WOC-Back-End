using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class WorkspaceWorkspacesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyViewModel Company { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        public WorkspaceWorkspacesViewModel(Workspace workspace)
        {
            Id = workspace.Id;
            Name = workspace.Name;
            Company = new(workspace.Company);
            Accounts = new();
            if (workspace.Accounts != null)
            {
                for (int i = 0; i < workspace.Accounts.Count; i++)
                {
                    Accounts.Add(new(workspace.Accounts[i].Account));
                }
            }
        }
    }
}
