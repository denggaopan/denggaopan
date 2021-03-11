using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Denggaopan.K8sConfigMapDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder => builder
                    .AddJsonFile("config/apollo.json")
                    .AddApollo(builder.Build().GetSection("apollo"))
                    .AddDefault()
                    .AddNamespace("paul")
                    .AddNamespace("deng"))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
