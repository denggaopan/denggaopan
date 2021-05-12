using Denggaopan.Helpers;
using System;
using System.Net.Http;

namespace Denggaopan.Net.Http.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var r = HttpHelper.GetAsync("https://www.baidu.com/");
            //Console.WriteLine(r.Result);


            var r1 = HttpHelper.GetAsync("http://localhost:56645/api/values");
            var r2 = HttpHelper.GetAsync("http://localhost:56645/api/values/9");
            var r3 = HttpHelper.PostAsync("http://localhost:56645/api/values","popo");
            var r4 = HttpHelper.PutAsync("http://localhost:56645/api/values/6", "pupu");
            var r5 = HttpHelper.DeleteAsync("http://localhost:56645/api/values/8");

            Console.WriteLine("================");
            Console.WriteLine(r1.Result);
            Console.WriteLine("================");
            Console.WriteLine(r2.Result);
            Console.WriteLine("================");
            Console.WriteLine(r3.Result);
            Console.WriteLine("================");
            Console.WriteLine(r4.Result);
            Console.WriteLine("================");
            Console.WriteLine(r5.Result);



            Console.ReadKey();
        }
    }
}
