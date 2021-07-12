using Denggaopan.Helpers;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;

namespace ConsoleApp1
{
    [Flags]
    enum OsType
    {
        [Description("微软Windows")]
        Windows = 1 << 0,

        [Description("苹果iOS")]
        Ios = 1 << 1,

        [Description("谷歌安卓")]
        Android = 1 << 2,

        [Description("华为鸿蒙")]
        HarmonyOS = 1 << 3,
    }
    class Program
    {
        static void Main(string[] args)
        {
            //dateTimeHelperTest();
            //Console.ReadLine();
            //return;



            Console.WriteLine("==================enum=================");
            var list = EnumHelper.GetList<OsType>();
            Console.WriteLine(JsonConvert.SerializeObject(list));

            var desc = OsType.HarmonyOS.GetDescription();
            Console.WriteLine(desc);

            var oss = OsType.Ios | OsType.HarmonyOS;
            Console.WriteLine(oss.ToString());


            var oss2 = (OsType)15;
            var oss2s = oss2.ToString();
            Console.WriteLine(oss2s);
            Console.WriteLine($"oss2包涵鸿蒙吗？{oss2.HasFlag(OsType.HarmonyOS)}。");
            var ostype = Enum.Parse(typeof(OsType), oss2s);
            Console.WriteLine($"ostype:{ostype} => {(int)ostype}");
            var ostype2 = Enum.Parse<OsType>(oss2s, ignoreCase: true);
            Console.WriteLine($"ostype2:{ostype2} => {(int)ostype2}");


            Console.WriteLine($"7&8:{Convert.ToString(7,2)}&{Convert.ToString(8, 2)} ==>{7 & (int)OsType.HarmonyOS}, { (7 & (int)OsType.HarmonyOS) == (int)OsType.HarmonyOS}");
            Console.WriteLine($"15&8:{Convert.ToString(15, 2)}&{Convert.ToString(8, 2)} ==>{15 & (int)OsType.HarmonyOS}, { (15 & (int)OsType.HarmonyOS) == (int)OsType.HarmonyOS}");

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

        private static void dateTimeHelperTest()
        {
            var date = new DateTime(2021, 6, 6, 0, 0, 0, DateTimeKind.Local);
            var dates1 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.Today);
            Console.WriteLine($"today: {JsonConvert.SerializeObject(dates1)}");

            var dates2 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.Today, date);
            Console.WriteLine($"today: {JsonConvert.SerializeObject(dates2)}");



            var dates3 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.Yesterday);
            Console.WriteLine($"Yesterday: {JsonConvert.SerializeObject(dates3)}");

            var dates4 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.Yesterday, date);
            Console.WriteLine($"Yesterday: {JsonConvert.SerializeObject(dates4)}");



            var dates5 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.ThisWeek);
            Console.WriteLine($"ThisWeek: {JsonConvert.SerializeObject(dates5)}");

            var dates6 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.ThisWeek, date);
            Console.WriteLine($"ThisWeek: {JsonConvert.SerializeObject(dates6)}");



            var dates7 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.LastWeek);
            Console.WriteLine($"LastWeek: {JsonConvert.SerializeObject(dates7)}");

            var dates8 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.LastWeek, date);
            Console.WriteLine($"LastWeek: {JsonConvert.SerializeObject(dates8)}");



            var dates9 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.In7Days);
            Console.WriteLine($"In7Days: {JsonConvert.SerializeObject(dates9)}");

            var dates10 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.In7Days, date);
            Console.WriteLine($"In7Days: {JsonConvert.SerializeObject(dates10)}");



            var dates11 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.ThisMonth);
            Console.WriteLine($"ThisMonth: {JsonConvert.SerializeObject(dates11)}");

            var dates12 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.ThisMonth, date);
            Console.WriteLine($"ThisMonth: {JsonConvert.SerializeObject(dates12)}");



            var dates13 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.LastMonth);
            Console.WriteLine($"LastMonth: {JsonConvert.SerializeObject(dates13)}");

            var dates14 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.LastMonth, date);
            Console.WriteLine($"LastMonth: {JsonConvert.SerializeObject(dates14)}");



            var dates15 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.ThisYear);
            Console.WriteLine($"ThisYear: {JsonConvert.SerializeObject(dates15)}");

            var dates16 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.ThisYear, date);
            Console.WriteLine($"ThisYear: {JsonConvert.SerializeObject(dates16)}");



            var dates17 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.LastYear);
            Console.WriteLine($"LastYear: {JsonConvert.SerializeObject(dates17)}");

            var dates18 = DateTimeHelper.GetDateTimeRange(DateTimeRangeType.LastYear, date);
            Console.WriteLine($"LastYear: {JsonConvert.SerializeObject(dates18)}");

            Console.WriteLine("------------------------------");
        }
    }
}
