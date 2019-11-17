using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOP_DispatchProxy.Services
{
    public class WelcomeService : IWelcomeService
    {
        public string SayHi()
        {
            Console.WriteLine("Hi,世界");
            return "Hi,世界";
        }
    }
}
