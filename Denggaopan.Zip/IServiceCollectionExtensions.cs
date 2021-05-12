using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.Zip
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddNpoi(this IServiceCollection services)
        {
            return services.AddSingleton<IZipService, ZipService>();
        }
    }
}
