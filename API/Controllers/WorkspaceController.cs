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
    }
}
