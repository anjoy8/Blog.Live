using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Configuration_All
{
    public class MyConfigProvider : ConfigurationProvider
    {
        /// <summary>
        /// 尝试从远程配置中心读取配置信息
        /// </summary>
        public async override void Load()
        {
            var response = "";
            try
            {
                var serverAddress = "http://localhost:5001";
                var client = new HttpClient();
                client.BaseAddress = new Uri(serverAddress);
                response = await client.GetStringAsync("/weatherforecast");
            }
            catch (Exception)
            {
                // 异常
            }

            if (string.IsNullOrEmpty(response))
            {
                throw new Exception("数据未响应.");
            }

            var configs = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(response);

            Data = new ConcurrentDictionary<string, string>();

            configs.ForEach(c =>
            {
                Data.Add(c);
            });
        }

    }
}
