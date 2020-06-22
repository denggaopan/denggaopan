using System;

namespace Denggaopan.TinyUrl.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var keys = TinyUrlHelper.GenerateKeys();
            Console.WriteLine(keys);


            var keys2 = TinyUrlHelper.GenerateKeys2();
            Console.WriteLine(keys2);


            //var num = 100000;
            //for (var i = num; i < 110000; i++)
            //{
            //    num += TinyUrlHelper.GetRnd();
            //    var key = TinyUrlHelper.Convert(num);
            //    var newKey = TinyUrlHelper.Mixup(key);
            //    var key2 = TinyUrlHelper.UnMixup(newKey);
            //    var num2 = TinyUrlHelper.Convert(key2);
            //    var keymix = TinyUrlHelper.Parse(num);
            //    var numix = TinyUrlHelper.Parse(keymix);
            //    Console.WriteLine($"{num}:{key}:{newKey}:{key2}:{num2}-{keymix}:{numix}");
            //}


            var count = 100;
            for (var i = 0; i < count; i++)
            {
                var num =(long) DateTime.Now.Subtract(new DateTime(1970,1,1)).TotalMilliseconds;
                var key = TinyUrlHelper.Convert(num);
                var newKey = TinyUrlHelper.Mixup(key);
                var key2 = TinyUrlHelper.UnMixup(newKey);
                var num2 = TinyUrlHelper.Convert(key2);
                var keymix = TinyUrlHelper.Parse(num);
                var numix = TinyUrlHelper.Parse(keymix);
                Console.WriteLine($"{num}:{key}:{newKey}:{key2}:{num2}-{keymix}:{numix}");
            }


            Console.ReadKey();
        }
    }
}
