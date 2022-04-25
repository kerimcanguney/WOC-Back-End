using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class JoinRequest
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
        public bool IsAccepted { get; set; }
    }
}
