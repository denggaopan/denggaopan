using FreeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.Redis
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration config)
        {
            var redisClient = new RedisClient(config["Redis:Conn"]);
            return services.AddSingleton(redisClient);
        }
    }
}
