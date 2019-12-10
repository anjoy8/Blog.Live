using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Configuration_All.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Configuration_All
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(new ConfigrationBuild());

            var appconfig = new Rootobject();
            Configuration.Bind(appconfig);

            services.AddSingleton(appconfig);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }



    public class Rootobject
    {
        public Logging Logging { get; set; }
        public OBA OBA { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class OBA
    {
        public A A { get; set; }
        public A_B A_b { get; set; }
    }

    public class A
    {
        public string Default { get; set; }
    }

    public class A_B
    {
        public string Default { get; set; }
    }


}
