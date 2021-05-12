using Denggaopan.QrCode.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.QrCode
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddQrCode(this IServiceCollection services, IConfiguration config)
        {
            var qrcodeConfig = config.GetSection("QrCode").Get<QrCodeConfig>();
            IQrCodeService srv = new QrCodeService(qrcodeConfig);
            return services.AddSingleton(srv);
        }
    }
}
