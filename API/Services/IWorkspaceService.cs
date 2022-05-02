using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IWorkspaceService
    {
        public List<Workspace> GetWorkspaces();
        public List<JoinRequest> GetJoinRequests();
        public bool Join(int userId, int workspaceId);
        public bool AcceptJoinRequest(int userId, int workspaceId);
        //public Workspace GetWorkspaceById(int id);
    }
}
