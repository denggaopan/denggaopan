using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Denggaopan.Snowflake;

namespace Denggaopan.Snowflake.Demo
{
    class Program
    {
        private static int N = 2000000;
        private static int TN = 1<<5 -1;
        private static HashSet<long> set = new HashSet<long>();
        private static IdGenerator gen = new IdGenerator(1, 1);
        private static int taskCount = 0;


        static void Main1(string[] args)
        {
            for (int i = 0; i <= TN; i++)
            {
                var ig = new IdGenerator(i, 1);
                Task.Run(() => GetID(ig));


            }
            Task.Run(() => Printf());

            var id = gen.NextId();
            Console.WriteLine(id);

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
        private static void GetID(IdGenerator gen)
        {
            for (var i = 0; i < N; i++)
            {
                var id = gen.NextId();

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
