using Denggaopan.GraphqlDemo.Models;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
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

        public GraphQLMiddleware(RequestDelegate next, IDocumentWriter writer, IDocumentExecuter executor)
        {
            _next = next;
            _writer = writer;
            _executor = executor;
        }

        public async Task InvokeAsync(HttpContext context, ISchema schema)
        {
            if (context.Request.Path.StartsWithSegments("/api/graphql") && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase))
            {
                var body = string.Empty;
                using (var streamreader = new StreamReader(context.Request.Body))
                {
                    body = await streamreader.ReadToEndAsync();
                }
                var req = JsonConvert.DeserializeObject<GraphqlRequest>(body);
                var result = await new DocumentExecuter()
                    .ExecuteAsync(doc =>
                    {
                        doc.Schema = schema;
                        doc.Query = req.Query;
                        doc.Inputs = req.Variables.ToInputs();
                    }).ConfigureAwait(false);

                var json = new DocumentWriter(indent: true).Write(result);
                await context.Response.WriteAsync(json);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
