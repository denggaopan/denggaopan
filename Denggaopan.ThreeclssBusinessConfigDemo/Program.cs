using System;
using System.Security.Cryptography;
using System.Text;
using Denggaopan.Net.Http;

namespace Denggaopan.ThreeclssBusinessConfigDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var rr = post("setBusinessConfig", new { UpdatorId = "2", BusinessGuid = "a6bfc910e9d04629b449654bc94d7256", IsActive = false, UpdatorName = "", Remark = "" });
            Console.WriteLine("get==>" + rr);
            Console.WriteLine("\n");

            //获取商家配置
            var result = post("getBusinessConfigList", new { });
            Console.WriteLine("get==>" + result);
            Console.WriteLine("\n");

            //获取商家配置
            var result1 = post("getBusinessConfig", new { BusinessGuid = "123456" });
            Console.WriteLine("get==>" + result1);
            Console.WriteLine("\n");

            //设置商家配置（有，更新；无，新增）
            var result2 = post("setBusinessConfig", new { BusinessGuid = "123456", IsActive = false, Remark = "xxx", UpdatorId = "1", UpdatorName = "PAUL" });
            Console.WriteLine("set==>" + result2);
            Console.WriteLine("\n");

            //设置商家配置（有，更新；无，新增）
            var result3 = post("setBusinessConfig", new { BusinessGuid = "123456789", IsActive = false, Remark = "xxx", UpdatorId = "1", UpdatorName = "PAUL" });
            Console.WriteLine("set==>" + result3);
            Console.WriteLine("\n");

            //设置商家配置（有，更新；无，新增）
            var result4 = post("setBusinessConfig", new { businessGuid = "a6bfc910e9d04629b449654bc94d7256", isActive = true, remark = "try3", updatorId = "1", updatorName = "PAUL" });
            Console.WriteLine("set==>" + result4);
            Console.WriteLine("\n");

            //设置商家配置（有，更新；无，新增）
            var result5 = post("setBusinessConfig", new { businessGuid = "86411bf7dd474c66aff290cab84ef0c3", isActive = false, remark = "金康'开发'平台", updatorId = "1", updatorName = "PAUL" });
            Console.WriteLine("set==>" + result5);
            Console.WriteLine("\n");



            Console.ReadLine();
        }

        private static string post(string url,object data)
        {
            var env = "dev";
            var appId = "thrclss001";
            var appSecret = "9ac65bd5f56d46e696af6302dc1ecd85";
            var apiUrl = string.Empty;
            switch(env)
            {
                case "local":
                    apiUrl = $"http://192.168.10.50:8801/api/threeclss/{url}";
                    break;
                case "dev":
                    apiUrl = $"http://dev.yun.kingcome.net.cn/api/threeclss/{url}";
                    break;
                default:
                    apiUrl = $"http://localhost:5001/api/threeclss/{url}";
                    break;
            }

            var timestamp = getTimestamp();
            
            var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var plainText = $"{dataJson}{appSecret}{timestamp}";
            var sign = md5(plainText).ToUpper();

            var fullUrl = apiUrl + $"?appid={appId}&timestamp={timestamp}&sign={sign}";

            var result = HttpHelper.PostAsync(fullUrl, dataJson).Result;
            return result;
        }


        private static long getTimestamp(DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }
            return (long)now.Value.Subtract(new DateTime(1970, 1, 1, 8, 0, 0)).TotalSeconds;
        }

        private static string md5(string plainText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] array = md5.ComputeHash(bytes);
            md5.Clear();
            string text = "";
            for (int i = 0; i < array.Length; i++)
            {
                text += array[i].ToString("x").PadLeft(2, '0');
            }
            return text;
        }
    }
}
