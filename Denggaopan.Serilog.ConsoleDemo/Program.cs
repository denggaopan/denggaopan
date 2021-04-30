using Serilog;
using Serilog.Formatting.Compact;
using System;

namespace Denggaopan.Serilog.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(new CompactJsonFormatter(), "log.clef")
            .CreateLogger();

            Log.Information("Hello, Serilog!");
            Log.Warning("Goodbye, Serilog.");

            var ex = new Exception("test");
            var msg = "{ \"@t\":\"2017-11-20T11:33:01.22138\",\"@m\":\"Hello, Serilog!\"}";
            Log.Error(ex,msg);

            var itemCount = 10;
            for (var itemNumber = 0; itemNumber < itemCount; ++itemNumber)
                Log.Debug("Processing item {ItemNumber} of {ItemCount}", itemNumber, itemCount);

            Log.CloseAndFlush();
        }
    }
}
