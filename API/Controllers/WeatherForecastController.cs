using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public string AdminEndpoint()
        {
            var currenUser = GetCurrentUserViaHttpContext();

            return "you are a admin 0_o";
        }

        [HttpGet("noob")]
        [Authorize(Roles = "noob")]
        public string NoobEndpoint()
        {
            var currenUser = GetCurrentUserViaHttpContext();

            return "gt yo goofy 💀 ahh  outa here man";
        }


        private Models.Account GetCurrentUserViaHttpContext()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null) return null;

            var userClaims = identity.Claims;

            return new Models.Account
            {
                Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                Role = new Models.Role() { Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value }

            };
        }
    }
}
