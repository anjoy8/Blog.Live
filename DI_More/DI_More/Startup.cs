using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI_More.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DI_More
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

            services.AddScoped<OneSeparateService>();
            //services.AddScoped<IMoreImplService, WelcomeChineseService>();
            //services.AddScoped<IMoreImplService, WelcomeEnglishService>();


            //SingletonFactory singletonFactory = new SingletonFactory();
            //singletonFactory.AddService<IMoreImplService>(new WelcomeChineseService(), "Chinese");
            //singletonFactory.AddService<IMoreImplService>(new WelcomeEnglishService(), "English");

            //services.AddSingleton(singletonFactory);


            services.AddSingleton<WelcomeChineseService>();
            services.AddSingleton<WelcomeEnglishService>();

            services.AddSingleton(factory =>
            {
                Func<string, IMoreImplService> accesor = key =>
                {
                    if (key.Equals("Chinese"))
                    {
                        return factory.GetService<WelcomeChineseService>();
                    }
                    else if (key.Equals("English"))
                    {
                        return factory.GetService<WelcomeEnglishService>();
                    }
                    else
                    {
                        throw new ArgumentException($"Not Support key : {key}");
                    }
                };
                return accesor;
            });


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
}
