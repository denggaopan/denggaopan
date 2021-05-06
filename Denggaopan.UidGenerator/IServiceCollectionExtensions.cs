using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Denggaopan.UidGenerator
{
    public static class IServiceCollectionExtensions
    {
        public static void AddUidGenerator(this IServiceCollection services, IConfiguration config)
        {
            var ip = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString();
            var list = ip.Split('.').Select(a => Convert.ToInt32(a)).ToList();
            var num = (list[2] << 8) | list[3];
            services.AddSingleton(sp => new UidGen(ip, num));
        }
    }
}
