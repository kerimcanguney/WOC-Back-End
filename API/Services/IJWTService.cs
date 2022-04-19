using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IJWTService
    {
        public string GenerateToken(Account account);
        public Account ValidateToken(ClaimsIdentity HttpContextIdentity);
    }
}
