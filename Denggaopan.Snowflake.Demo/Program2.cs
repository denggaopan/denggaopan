using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Denggaopan.Snowflake;

namespace Denggaopan.Snowflake.Demo
{
    class Program2
    {
        private static int N = 2000000;
        private static int TN = 30;
        private static HashSet<long> set = new HashSet<long>();
        private static HashSet<long> set2 = new HashSet<long>();
        private static int taskCount = 0;


        static void Main(string[] args)
        {
            for (int i = 0; i <= TN; i++)
            {
                Task.Run(() => GetID());
            }
            Task.Run(() => Printf());

            //for (var i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(DateTime.Now.ToString("ffffff"));
            //}

            Console.ReadKey();
        }

        private static void Printf()
        {
            while (taskCount != TN)
            {
                Console.WriteLine("...");
                Thread.Sleep(1000);
            }
            Console.WriteLine(set.Count == N * taskCount);
        }

        private static object o = new object();
        private static void GetID()
        {
            var client = new HttpClient();
            for (var i = 0; i < N; i++)
            {
                var apiUrl = "http://localhost:53692/api/Sample"; //http://localhost:8101/sample/Snowflake
                var id = long.Parse(client.GetStringAsync(apiUrl).Result);

                lock (o)
                {
                    if (set.Contains(id))
                    {
                        set2.Add(id);
                    }
                    else
                    {
                        set.Add(id);
                    }
                }

            }
            Console.WriteLine($"任务{++taskCount}完成");
        }
    }
}
