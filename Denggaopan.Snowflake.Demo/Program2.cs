using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Denggaopan.Snowflake;

namespace Denggaopan.Snowflake.Demo
{
    class Program2
    {
        private static int N = 100;
        private static int TN = 1;
        private static HashSet<long> set = new HashSet<long>();
        private static HashSet<long> set2 = new HashSet<long>();
        private static int taskCount = 0;


        static void Main(string[] args)
        {
            Task.Run(() => Printf());
            for (int i = 0; i < TN; i++)
            {
                Task.Run(() => GetID());
            }

            Console.ReadKey();
        }

        private static void Printf()
        {
            var sw = new Stopwatch();
            sw.Start();
            while (taskCount != TN)
            {
                Console.WriteLine("...");
                Thread.Sleep(1000);
            }

            var success = set.Count == N * taskCount;
            Console.WriteLine(success ? "生成成功" : "生成失败");
            if (!success)
            {
                Console.WriteLine(set2);
            }
            sw.Stop();
            Console.WriteLine((decimal)sw.ElapsedMilliseconds / 1000 + "s");

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
                //Console.WriteLine($"{id} => {TinyUrl.TinyUrlHelper.Parse(id)}");
            }
            Console.WriteLine($"任务{++taskCount}完成");
        }
    }
}
