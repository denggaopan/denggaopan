using Denggaopan.GraphqlDemo.Models;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Middlewares
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDocumentWriter _writer;
        private readonly IDocumentExecuter _executor;
        private readonly ILogger<GraphQLMiddleware> _logger;

        public GraphQLMiddleware(RequestDelegate next, IDocumentWriter writer, IDocumentExecuter executor, ILogger<GraphQLMiddleware> logger)
        {
            _next = next;
            _writer = writer;
            _executor = executor;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ISchema schema,IServiceProvider serviceProvider)
        {
            if (context.Request.Path.StartsWithSegments("/graphql") && context.Request.Method.ToUpper() == "POST")
            {
                var body = string.Empty;
                using (var streamreader = new StreamReader(context.Request.Body))
                {
                    body = await streamreader.ReadToEndAsync();
                }
                _logger.LogError($"req.body:{body}");
                var req = JsonConvert.DeserializeObject<GraphqlRequest>(body);
                var result = await new DocumentExecuter()
                    .ExecuteAsync(doc =>
                    {
                        doc.Schema = schema;
                        doc.Query = req.Query;
                        doc.Inputs = req.Variables.ToInputs();
                        doc.Listeners.Add(serviceProvider.GetRequiredService<DataLoaderDocumentListener>());
                    }).ConfigureAwait(false);

                var json = new DocumentWriter(indent: true).Write(result);
                _logger.LogError($"response:{json}");
                await context.Response.WriteAsync(json);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
