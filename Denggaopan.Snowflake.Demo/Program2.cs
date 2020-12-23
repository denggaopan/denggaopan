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
        private static IdGenerator gen = new IdGenerator(1, 1);
        private static int taskCount = 0;


        static void Main2(string[] args)
        {
            for (int i = 0; i <= TN; i++)
            {
                Task.Run(() => GetID());


            }
            Task.Run(() => Printf());

            var id = gen.NextId();
            Console.WriteLine(id);

            var ts = 1400515200000L;// 1604367042000L;
            var tsbit = Convert.ToString(ts, 2);
            Console.WriteLine($"{ts} => {tsbit}");

            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine(DateTime.Now.ToString("ffffff"));
            }

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
                var id = long.Parse(client.GetStringAsync("http://localhost:8101/sample/Snowflake").Result);

                lock (o)
                {
                    if (set.Contains(id))
                    {
                        Console.WriteLine("发现重复项 : {0}", id);
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
