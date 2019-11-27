using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Serilog_Simple
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var log=new LoggerConfiguration()
            //                   .WriteTo.Console()
            //                   .CreateLogger() )
            //{
            //    log.Information("serilog simple use");

            //}

            var conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=others;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var tablName = "logs";

            // ×Ô¶¨Òå×Ö¶Î
            var columnOptions = new ColumnOptions
            {
                AdditionalDataColumns = new Collection<DataColumn>
            {
                new DataColumn {DataType = typeof (string), ColumnName = "User"},
                new DataColumn {DataType = typeof (string), ColumnName = "Cla"},
            }
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .WriteTo.Console()
                .WriteTo.File(Path.Combine("Logs", @"logxxx.log"), rollingInterval: RollingInterval.Infinite)

                .WriteTo.MSSqlServer(conn, tablName,autoCreateSqlTable:true, columnOptions: columnOptions, restrictedToMinimumLevel:LogEventLevel.Debug)

                

                .CreateLogger()
                ;

            Log.Information("serilog more config");

            var a = "a";
            var boy = "boy";
            Log.Information("i am {a} {b}", a, boy);


            var name = "zs";
            var age = 18;
            Log.Information("i am {a},> {b}", name, age>=18);


            var Model = new { Name = "laozhang", Age = 19 };

            Log.Information("data: {model}", Model);
            Log.Information("data: {@model}", Model);


            Log.CloseAndFlush();



            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseSerilog()
                    .UseStartup<Startup>();
                });
    }
}
