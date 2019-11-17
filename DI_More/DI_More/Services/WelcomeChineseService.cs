using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI_More.Services
{
    public class WelcomeChineseService : IMoreImplService
    {
        public string SayWelocome()
        {
            return "欢迎";
        }
    }
}
