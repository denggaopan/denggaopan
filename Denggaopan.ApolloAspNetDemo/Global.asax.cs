using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.ConfigAdapter;
using Com.Ctrip.Framework.Apollo.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Denggaopan.ApolloAspNetDemo
{
    public class Global : HttpApplication
    {
        public static IConfiguration Configuration { get; private set; }
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region apollo
            YamlConfigAdapter.Register();

            var apolloAppId = ConfigurationManager.AppSettings["Apollo.AppId"];
            var apolloEnv = ConfigurationManager.AppSettings["Apollo.Env"];
            if (!string.IsNullOrEmpty(apolloEnv))
            {
                apolloEnv = apolloEnv.ToUpper();
            }
            else
            {
                apolloEnv = "DEV";
            }
            var apolloMetaServer = ConfigurationManager.AppSettings[$"Apollo.{apolloEnv}.Meta"];
            Configuration = new ConfigurationBuilder()
                .AddApollo(apolloAppId, apolloMetaServer)
                .AddDefault(ConfigFileFormat.Xml)
                .AddDefault(ConfigFileFormat.Json)
                .AddDefault(ConfigFileFormat.Yml)
                .AddDefault(ConfigFileFormat.Yaml)
                .AddDefault()
                .Build();
            #endregion
        }
    }
}