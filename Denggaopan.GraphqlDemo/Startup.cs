using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Denggaopan.GraphqlDemo.Models;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Newtonsoft.Json;
using Denggaopan.GraphqlDemo.Middlewares;
using Denggaopan.GraphqlDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using GraphQL.DataLoader;
using Microsoft.AspNetCore.Mvc;

namespace Denggaopan.GraphqlDemo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddScoped<IDataStore, DataStore>();
            services.AddScoped<InventoryQuery>();
            services.AddScoped<InventoryMutation>();
            services.AddScoped<ISchema, InventorySchema>();

            services.AddScoped<ItemType>();
            services.AddScoped<ItemInputType>();
            services.AddScoped<OrderType>();
            services.AddScoped<OrderInputType>();
            services.AddScoped<CustomerType>();
            services.AddScoped<CustomerInputType>();
            services.AddScoped<OrderItemType>();
            services.AddScoped<OrderItemInputType>();

            services.AddScoped<IDependencyResolver>(s =>
                new FuncDependencyResolver(s.GetRequiredService));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext db)
        {
            //have migrations,update database
            if (db.Database.GetPendingMigrations().Any())
            {
                db.Database.Migrate();
            }

            //graphql
            app.UseMiddleware<GraphQLMiddleware>();

            //rest
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
