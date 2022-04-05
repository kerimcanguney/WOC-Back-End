using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class WorkspaceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CompanyViewModel Company { get; set; }
        //public List<AccountViewModel> Accounts { get; set; }
        public WorkspaceViewModel(Workspace workspace)
        {
            Id = workspace.Id;
            Name = workspace.Name;
            Company = new(workspace.Company);
        }
    }
}
