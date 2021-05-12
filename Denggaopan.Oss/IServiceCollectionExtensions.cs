using Denggaopan.Oss.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.Oss
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddHuaweiObs(this IServiceCollection services, IConfiguration config)
        {
            var obsConfig = config.GetSection("HuaweiObs").Get<HuaweiObsConfig>();
            IObjectStorageService srv = new HuaweiObjectStorageService(obsConfig);
            return services.AddSingleton(srv);
        }

        public static IServiceCollection AddAliyunOss(this IServiceCollection services, IConfiguration config)
        {
            var ossConfig = config.GetSection("AliyunOss").Get<AliyunOssConfig>();
            IObjectStorageService srv = new AliyunObjectStorageService(ossConfig);
            return services.AddSingleton(srv);
        }
    }
}
