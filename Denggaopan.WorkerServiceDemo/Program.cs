using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Denggaopan.WorkerServiceDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;

namespace Denggaopan.WorkerServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args);
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            if (isWindows)
            {
                host.UseWindowsService();
            }
            else
            {
                host.UseSystemd();
            }
            host.ConfigureServices((hostContext, services) =>
            {
                //services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(hostContext.Configuration.GetConnectionString("default")));
                services.AddSingleton<DbContext,ApplicationDbContext>();
                //services.AddScoped<DbContext>(provider => provider.GetService<ApplicationDbContext>());

                services.AddLogging(builder =>
                {
                    builder.AddNLog();
                });

                services.AddHostedService<Worker>();
                //services.AddHostedService<WorkerPro>();
            });

            return host;
        }
    }
}
