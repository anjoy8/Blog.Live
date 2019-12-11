using Microsoft.Extensions.Configuration;

namespace Configuration_All
{
    public class MyConfigSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MyConfigProvider();
        }
    }
}