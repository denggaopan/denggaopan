using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.Npoi
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonNpoi(this IServiceCollection services)
        {
            return services.AddSingleton<IExcelService, ExcelService>();
        }
    }
}
