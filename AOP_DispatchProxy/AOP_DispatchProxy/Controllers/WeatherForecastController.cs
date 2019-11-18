using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOP_DispatchProxy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AOP_DispatchProxy.Controllers
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
        private readonly IWelcomeService _welcomeService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWelcomeService welcomeService)
        {
            _logger = logger;
            _welcomeService = welcomeService;
        }

        [HttpGet]
        public object Get()
        {
            return _welcomeService.SayHi();
        }
    }
}
