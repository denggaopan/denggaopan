using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormRequest
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                var obj = new { name = "paul", age = 18 };
                var url = "http://192.168.10.51:8091/notify/reserve/hello";
                var list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("", "xxxx"));
                var content = new FormUrlEncodedContent(list);

                HttpClient client = new HttpClient();

                var res = client.PostAsync(url, content);
                var rm = res.Result;
                Console.WriteLine(rm.StatusCode);
                if (rm.IsSuccessStatusCode)
                {
                    var str = rm.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(str);
                }
                client.Dispose();
                Console.WriteLine("--------------------");
                Thread.Sleep(3 * 1000);
            }
        }
    }
}
