using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ConsoleApp1
{
    public class HttpHelpler
    {
        public static string Post(string url, string param)
        {
            using (HttpClient client = new HttpClient())
            {
                var list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("", param));
                var content = new FormUrlEncodedContent(list);
                var res = client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result;
                return res;
            }
        }
    }
}
