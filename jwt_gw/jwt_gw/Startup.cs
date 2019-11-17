using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jwt_gw.PolicyRequirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace jwt_gw
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


            services.AddAuthorization(o =>
           {
               // 策略授权 三大块
               // 基于角色
               o.AddPolicy("AdminPolicy1", o =>
               {
                   o.RequireRole("Admin").Build();//cliamtype=Role
               });

               // 2.基于声明
               o.AddPolicy("AdminClaim2", o => {
                   o.RequireClaim("Email","Admin@qq.com","User@qq.com");//claimType
                   o.RequireClaim("laozhang", "laoli");//claimType
               });

               // 3.基于需求Requirement
               o.AddPolicy("AdminRequireMent",o=> {
                   o.Requirements.Add(new AdminRequirement() { Name="laozhang"});//完全自定义
               });


           });

            services.AddSingleton<IAuthorizationHandler,MustRoleAdminHandler>();






            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("laozhanglaozhanglaozhanglaozhang"));
            services.AddAuthentication("Bearer")
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // 3 + 2 

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,

                        ValidateIssuer = true,
                        ValidIssuer = "issuer",

                        ValidateAudience = true,
                        ValidAudience = "audience",



                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                    };
                })


                ;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
