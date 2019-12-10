using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration_All.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configuration_All.Controllers
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
        private readonly Rootobject _rootobject;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Rootobject rootobject)
        {
            _logger = logger;
            _rootobject = rootobject;
        }

        [HttpGet]
        public object Get()
        {
            return ConfigrationBuild.GetVal("Chinese") +
                ConfigrationBuild.GetValBySection("Chinese") +
               JsonConvert.SerializeObject(_rootobject.Logging.LogLevel)
                ;
        }
    }
}
