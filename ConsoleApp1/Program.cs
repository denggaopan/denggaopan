using Denggaopan.Helpers;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;

namespace ConsoleApp1
{
    enum OsType
    {
        [Description("微软Windows")]
        Windows,

        [Description("苹果iOS")]
        Ios,

        [Description("谷歌安卓")]
        Android
    }
    class Program
    {
        static void Main(string[] args)
        {

            var list = EnumHelper.GetList<OsType>();
            Console.WriteLine(JsonConvert.SerializeObject(list));

            var desc = OsType.Ios.GetDescription();
            Console.WriteLine(desc);
            Console.ReadLine();
            return;

            var a = Convert.ToString(100, 2);
            Console.WriteLine(a);
            return;

            var url = $"https://dev.king.kingcome.net.cn/notify/reserve/PaidMoneyRefund";
            var data = new
            {
                orderId = 5164,
                reason="xxxxx",
                employeeId = 5541,
                employeeName = "PAUL"
            };
            var res = HttpHelpler.Post(url, JsonConvert.SerializeObject(data));
            Console.WriteLine(res);
            return;

            Console.WriteLine("Hello World!");

            var ts1 = 1400515200000L;
            var ts2 = 1604368569296L;
            var t = ts2 - ts1;
            var b1 = Convert.ToString(ts1, 2);
            var b2 = Convert.ToString(ts2, 2);
            var b = Convert.ToString(t, 2);
            Console.WriteLine($"{b1}");
            Console.WriteLine($"{b2}");
            Console.WriteLine($"{b}");

            var ts3 = 1400515200L;
            var ts4 = 1604368782L;
            var t2 = ts4 - ts3;
            var b3 = Convert.ToString(ts3, 2);
            var b4 = Convert.ToString(ts4, 2);
            var b_2 = Convert.ToString(t2, 2);
            Console.WriteLine($"{b3}");
            Console.WriteLine($"{b4}");
            Console.WriteLine($"{b_2}");

            var myarr = new string[] { "10", "20", "30", "40", "50", "60", "70", "80", "90", "100" };
            //1. 获取数组 前3个元素
            var query1 = myarr[0..3];

            var query2 = myarr.Take(3).ToList();

            Console.WriteLine($"query1={string.Join(",", query1)}");
            Console.WriteLine($"query2={string.Join(",", query2)}");


            //2. 获取数组 最后3个元素
            var query3 = myarr[^3..];

            var query4 = myarr.Skip(myarr.Length - 3).ToList();

            Console.WriteLine($"query3={string.Join(",", query3)}");
            Console.WriteLine($"query4={string.Join(",", query4)}");

            //1. 获取数组 中 index=4,5,6 三个位置的元素
            var query5 = myarr[4..7];

            var query6 = myarr.Skip(4).Take(3).ToList();

            Console.WriteLine($"query5={string.Join(",", query5)}");
            Console.WriteLine($"query6={string.Join(",", query6)}");


            //1. 获取 array 中倒数第三和第二个元素
            var query7 = myarr[^3..^1];
            var query8 = myarr.Skip(myarr.Length - 3).Take(2).ToList();

            Console.WriteLine($"query7={string.Join(",", query7)}");
            Console.WriteLine($"query8={string.Join(",", query8)}");


            Console.Read();
        }
    }
}
