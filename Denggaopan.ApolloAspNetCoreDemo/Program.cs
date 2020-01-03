using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.ConfigAdapter;
using Com.Ctrip.Framework.Apollo.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Denggaopan.ApolloAspNetCoreDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            YamlConfigAdapter.Register();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder => builder //普通方式，一般配置在appsettings.json中
                   .AddApollo(builder.Build().GetSection("apollo"))
                    .AddDefault(ConfigFileFormat.Xml)
                    .AddDefault(ConfigFileFormat.Json)
                    .AddDefault(ConfigFileFormat.Yml)
                    .AddDefault(ConfigFileFormat.Yaml)
                    .AddDefault())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
