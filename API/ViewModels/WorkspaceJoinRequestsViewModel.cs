using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class WorkspaceJoinRequestsViewModel
    {
        public AccountViewModel Account { get; set; }

        public WorkspaceViewModel Workspace { get; set; }
        public bool IsAccepted { get; set; }
        public WorkspaceJoinRequestsViewModel(JoinRequest joinRequest)
        {
            if (joinRequest.Account != null)
            {
                Account = new(joinRequest.Account);
            }
            if (joinRequest.Workspace != null)
            {
                Workspace = new(joinRequest.Workspace);
            }
            IsAccepted = joinRequest.IsAccepted;
        }
    }
}
