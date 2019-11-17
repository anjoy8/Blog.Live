using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI_More.Services
{
    public class WelcomeEnglishService : IMoreImplService
    {
        public string SayWelocome()
        {
            return "Welcome";
        }
    }
}
