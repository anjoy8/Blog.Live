using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AOP_DispatchProxy.Ex;
using AOP_DispatchProxy.Services;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AOP_DispatchProxy
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

            // 1
            //services.AddScoped<IWelcomeService, WelcomeService>();

            // 2
            //IWelcomeService decorator = DispatchProxy.Create<IWelcomeService, GenericDecorator>();
            //((GenericDecorator)decorator).TargetClass = new WelcomeService();
            //decorator.SayHi();

            // 3
            //services.AddScoped<WelcomeService>();
            //services.AddScoped<GenericDecorator>();
            //services.AddScoped<IWelcomeService>(provider =>
            //{
            //    var generator = new ProxyGenerator();
            //    var target = provider.GetService<WelcomeService>();
            //    var interceptor = provider.GetService<GenericDecorator>();
            //    var proxy = generator.CreateInterfaceProxyWithTarget<IWelcomeService>(target, interceptor);

            //    return proxy;
            //});



            // 4
            Assembly assembly = Assembly.Load("AOP_DispatchProxy");
            List<Type> ts = assembly.GetTypes().Where(d=>d.Name.EndsWith("Service")).ToList();

            foreach (var item in ts.Where(s => !s.IsInterface))
            {
                var interfaceType = item.GetInterfaces();
                foreach (var typeArray in interfaceType)
                {
                    services.AddScoped(typeArray, item);

                    services.AddScoped<WelcomeService>();
                    services.AddScoped<GenericDecorator>();
                    services.AddScoped<IWelcomeService>(provider =>
                    {
                        var generator = new ProxyGenerator();
                        var target = provider.GetService<WelcomeService>();
                        var interceptor = provider.GetService<GenericDecorator>();
                        var proxy = generator.CreateInterfaceProxyWithTarget<IWelcomeService>(target, interceptor);

                        return proxy;
                    });



                }
            }

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
