using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Denggaopan.ThreeclssSignGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //448078357cbe4066a8b0e8a4ef2f16f4 & 1576461799 & threeclsstoken200 &{ "businessguid":"86411bf7dd474c66aff290cab84ef0c2","storeguid":"6154a6543bca447f9999645508153c97","storename":"16-技术测试店","openid":"o_P8rsyS5CZr3RcvMbuqWatlaB10","orderid":"2883235","type":1,"amount":100.0}

            var distribution_api_secret = "threeclsstoken200";

            var data =new { businessguid="86411bf7dd474c66aff290cab84ef0c2",
                storeguid = "6154a6543bca447f9999645508153c97",
                storename="16-技术测试店",
                openid="o_P8rsyS5CZr3RcvMbuqWatlaB10",
                orderid="2883235",
                type=1,
                amount=100.0
            };
            var json = JsonConvert.SerializeObject(data);
            Console.WriteLine($"json:{json}");

            var text1 = "{\"businessguid\":\"86411bf7dd474c66aff290cab84ef0c2\",\"storeguid\":\"6154a6543bca447f9999645508153c97\",\"storename\":\"16-技术测试店\",\"openid\":\"o_P8rsyS5CZr3RcvMbuqWatlaB10\",\"orderid\":\"2883235\",\"type\":1,\"amount\":100.0}";
            var text2 = "{\"businessguid\":\"86411bf7dd474c66aff290cab84ef0c2\",\"storeguid\":\"6154a6543bca447f9999645508153c97\",\"storename\":\"16-技术测试店\",\"openid\":\"o_P8rsyS5CZr3RcvMbuqWatlaB10\",\"orderid\":\"2883235\",\"type\":1,\"amount\":100.0}";

            /*签名*/
            var noncestr = "448078357cbe4066a8b0e8a4ef2f16f4";
            var timestamp = 1576461799;
            var plainText1 = $"{noncestr}&{timestamp}&{distribution_api_secret}&{json}";
            var signature1 = KCTools.Security.MD5.Encrypt(plainText1).ToUpper();
            Console.WriteLine($"signature1:{signature1}");

            var plainText2 = $"{noncestr}&{timestamp}&{distribution_api_secret}&{text1}";
            var signature2 = KCTools.Security.MD5.Encrypt(plainText1).ToUpper();
            Console.WriteLine($"signature2:{signature2}");

            var plainText3 = $"{noncestr}&{timestamp}&{distribution_api_secret}&{text2}";
            var signature3 = KCTools.Security.MD5.Encrypt(plainText1).ToUpper();
            Console.WriteLine($"signature3:{signature1}");

            var auth = $"noncestr=\"{noncestr}\"&timestamp=\"{timestamp}\"&signature=\"{signature1}\"";
            var headers = new Dictionary<string, string>();
            headers.Add("X-Authorization", auth);

            Console.ReadLine();
        }

        private static long getTimestamp()
        {
            return (long)(DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0)).TotalSeconds;
        }
    }
}
