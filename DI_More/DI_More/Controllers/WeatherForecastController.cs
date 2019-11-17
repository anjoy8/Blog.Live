using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI_More.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI_More.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly OneSeparateService oneSeparateService;
        private readonly IMoreImplService moreImplService;
        private readonly IEnumerable<IMoreImplService> moreImplServices;
        private readonly SingletonFactory singletonFactory;
        private readonly IMoreImplService moreImplServiceChinese;
        private readonly IMoreImplService moreImplServiceEnglish;


        public WeatherForecastController(OneSeparateService oneSeparateService, SingletonFactory singletonFactory)
        {
            this.oneSeparateService = oneSeparateService;
            this.singletonFactory = singletonFactory;

            moreImplServiceChinese = singletonFactory.GetService<IMoreImplService>("Chinese");
            moreImplServiceEnglish = singletonFactory.GetService<IMoreImplService>("English");
        }

        [HttpGet]
        public object Get()
        {
            //OneSeparateService oneSeparateService = new OneSeparateService(); 
            return oneSeparateService.SayHello("English");
        }


        [HttpGet("/welcome")]
        public object GetWelcome()
        {
            var result = "";
            //foreach (var item in moreImplServices)
            //{
            //    result += item.SayWelocome();
            //}

            return moreImplServiceChinese.SayWelocome();
        }
    }
}
