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
        public WorkspaceViewModel(Workspace workspace)
        {
            Id = workspace.Id;
            Name = workspace.Name;
            if (Company != null)
            {
                Company = new(workspace.Company);
            }
        }
    }
}
