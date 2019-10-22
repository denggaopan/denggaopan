using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Registry;

namespace Denggaopan.PollyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}==> started");

            var exceptionPolicy = Policy<object>.Handle<Exception>();
            var exceptionErrPolicy = Policy<object>.Handle<Exception>(ex => ex.Message.Contains("err"));
            var timeoutPolicy = Policy.Timeout(10);

            dosth();
            var R5 = exceptionPolicy
                .Retry(5, (res, i, c) =>
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}==> R5 retry {i} times,ex:{res.Exception?.Message}");
                });

            var R5_2 = exceptionErrPolicy
                .Retry(5, (res, i, c) =>
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}==> R5-2 retry {i} times,ex:{res.Exception?.Message}");
                });

            var WR5 = exceptionPolicy
                .WaitAndRetry(5, retryTimes =>TimeSpan.FromSeconds(Math.Pow(2, retryTimes)), (res, t,i, c) =>
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}==> WR5 retry {i} times,ex:{res.Exception?.Message}");
                });

            var WR5_2 = exceptionPolicy
                .WaitAndRetry(5, retryTimes => TimeSpan.FromSeconds(5* retryTimes), (res, i, c) =>
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}==> WR5-2 retry {i} times,ex:{res.Exception?.Message}");
                });

            var WR5_3 = exceptionPolicy
                 .WaitAndRetry(new[] {
                     TimeSpan.FromSeconds(1),
                     TimeSpan.FromSeconds(2),
                     TimeSpan.FromSeconds(3),
                     TimeSpan.FromSeconds(5),
                     TimeSpan.FromSeconds(8)
                 }, (res, i, c) =>
                 {
                     Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}==> WR5 retry {i} times,ex:{res.Exception?.Message}");
                 });
            


            var RF = exceptionPolicy
                .RetryForever( (res, i, c) =>
             {
                 Console.WriteLine($"RF retry {i} times,ex:{res.Exception?.Message}");
             });

            PolicyRegistry registry = new PolicyRegistry();
            registry.Add("R5", R5);
            registry.Add("WR5", WR5);
            registry.Add("R5-2", R5_2);
            registry.Add("WR5-3", WR5_3);

            var policy = registry.Get<Policy<object>>("WR5");
            //var result = policy.Execute(() => {
            //    return xcalc(1000, 10);
            //});

            var result = timeoutPolicy.Execute<object>(() => {
                return timeoutSimulater(20);
            });


            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}==> {result}");
            Console.ReadKey();
            
        }

        private static string xcalc(int maxValue,int findValue)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var n = random.Next(0, maxValue);
            var m = findValue;
            var x = n % m;
            switch (x)
            {
                case 0:
                    return "success";
                default:
                    throw new Exception($"error:{n}/{m} = {x}");
            }
        }

        private static string timeoutSimulater(int seconds)
        {
            Thread.Sleep(seconds*1000);
            return "success";
        }


        private static void dosth()
        {
            Console.WriteLine("do something.");
            return;
        }
    }
}
