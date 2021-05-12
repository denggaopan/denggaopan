using Denggaopan.RabbitMq.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.RabbitMq
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddQrCode(this IServiceCollection services, IConfiguration config)
        {
            var qrcodeConfig = config.GetSection("RabbitMq").Get<RabbitMqConfig>();
            IRabbitMqService srv = new RabbitMqService(qrcodeConfig);
            return services.AddSingleton(srv);
        }
    }
}
