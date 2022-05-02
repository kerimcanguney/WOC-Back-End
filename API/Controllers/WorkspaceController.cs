using API.Data;
using API.Models;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceService _service;
        private readonly IJWTService _jwt;

        public WorkspaceController(IAccountContext context, IConfiguration config)
        {
            _service = new AccountService(context);
            _jwt = new JWTService(config);
        }
        [HttpGet("Workspaces")]
        public IEnumerable<WorkspaceWorkspacesViewModel> Workspaces()
        {
            List<Workspace> ws = _service.GetWorkspaces();
            List<WorkspaceWorkspacesViewModel> wsvms = new();
            for (int i = 0; i < ws.Count(); i++)
            {
                wsvms.Add(new(ws[i]));
            }
            return wsvms;
        }
        [HttpGet("JoinRequests")]
        public IEnumerable<WorkspaceJoinRequestsViewModel> JoinRequests()
        {
            List<JoinRequest> jr = _service.GetJoinRequests();
            List<WorkspaceJoinRequestsViewModel> jrvms = new();
            for (int i = 0; i < jr.Count(); i++)
            {
                jrvms.Add(new(jr[i]));
            }
            return jrvms;
        }
        //Create a join request to join a workspace
        [HttpPost("JoinWorkspace")]
        public bool JoinWorkspace(int userId, int workspaceId)
        {
            return _service.Join(userId,workspaceId);
        }
        //Accept a joinrequest
        [HttpPost("AcceptJoinRequest")]
        public bool AcceptJoinRequest(int userId, int workspaceId)
        {
            return _service.AcceptJoinRequest(userId, workspaceId);
        }
    }
}
