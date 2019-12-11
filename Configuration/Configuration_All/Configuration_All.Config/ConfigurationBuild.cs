using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;

namespace Configuration_All.Config
{
    public class ConfigurationBuild
    {
        static IConfiguration configuration { get; set; }
        public ConfigurationBuild()
        {
            var source = new Dictionary<string, string>
            {
                ["English"] = "hello",
                ["Chinese"] = "你好",
                ["Italian"] = "Ciao",
            };

            configuration = new ConfigurationBuilder()
               .Add(new MemoryConfigurationSource { InitialData = source })
               .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
               //.Add(new MyConfigSource())
               //.SetBasePath(@"c:\\ddd\\dddd")
               .Build()
               .GetSection("Logging")
               ;
        }


        public static string GetVal(string key)
        {
            return configuration[key];
        }
    }
}
