using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI_More.Services
{
    public class SingletonFactory
    {

        Dictionary<Type, Dictionary<string, object>> serviceDict;
        public SingletonFactory()
        {
            serviceDict = new Dictionary<Type, Dictionary<string, object>>();
        }

        public TService GetService<TService>(string id) where TService : class
        {
            var serviceType = typeof(TService);
            if (serviceDict.TryGetValue(serviceType, out Dictionary<string, object> implDict))
            {
                if (implDict.TryGetValue(id, out object service))
                {
                    return service as TService;
                }
            }
            return null;
        }

      

        public void AddService<TService>(TService service, string id) where TService : class
        {
            var serviceType = typeof(TService);
            if (service != null)
            {
                if (serviceDict.TryGetValue(serviceType, out Dictionary<string, object> implDict))
                {
                    implDict[id] = service;
                }
                else
                {
                    implDict = new Dictionary<string, object>();
                    implDict[id] = service;
                    serviceDict[serviceType] = implDict;
                }
            }
        }
    }
}
