using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration_All.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Configuration_All.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Rootobject _rootobject;
        private readonly IOptions<Logging> _options;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(Rootobject rootobject,IOptionsSnapshot<Logging> options,IConfiguration configuration)
        {
            _rootobject = rootobject;
            _options = options;
            _configuration = configuration;
        }

        [HttpGet]
        public object[] Get()
        {
            return new object[] { _rootobject?.Logging?.LogLevel,
            _options.Value.LogLevel,
            ConfigurationBuild.GetVal("Chinese"),
            _configuration["pwd"]
            };
        }
    }
}
