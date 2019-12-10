using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;

namespace Configuration_All.Config
{
    public class ConfigrationBuild
    {
        static IConfiguration Configuration { get; set; }

        public ConfigrationBuild()
        {
            var source = new Dictionary<string, string>
            {
                ["English"] = "Hello",
                ["Chinese"] = "你好",
                ["Italian"] = "Ciao",
                ["Chinese:Italian"] = "你好Ciao",
            };

            Configuration = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                //.SetBasePath(AppContext.BaseDirectory)
                //.Add(new JsonConfigurationSource { Path = "", Optional = false, ReloadOnChange = true })
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                .Build()
                .GetSection("Chinese");
        }

        public static string GetVal(string key)
        {
            return Configuration[key];
        }
        public static object GetValBySection(string key)
        {
            return Configuration.GetSection(key).Value;
        }
    }
}
