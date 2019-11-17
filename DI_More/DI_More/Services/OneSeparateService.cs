using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI_More.Services
{
    public class OneSeparateService
    {
        public string SayHello(string type="") {

            if (type == "English")
            {
                return "Hello";
            }
            else { 
                return "ni hao";

            }
        }
    }
}
