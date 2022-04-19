using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class JWTService : IJWTService
    {
        private IConfiguration _config;
        public JWTService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Name),
                new Claim(ClaimTypes.Email, account.Email),
                //new Claim(ClaimTypes.Role, account.Role.Name) 
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public Account ValidateToken(ClaimsIdentity HttpContextIdentity)
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            var identity = HttpContextIdentity;

            if (identity == null) return null;

            var userClaims = identity.Claims;

            return new Account
            {
                Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                //Role = new Role() { Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value }

            };
        }
    }
}
