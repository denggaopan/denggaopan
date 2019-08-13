using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Denggaopan.Net.Http
{
    public class HttpHelper
    {
        public static async Task<string> GetAsync(string url)
        {
            HttpClient client = new HttpClient();
            var r = await client.GetAsync(url);
            if (r.IsSuccessStatusCode)
            {
                var s = r.Content.ReadAsStringAsync().Result;
                return s;
            }
            return null;
        }

        public static async Task<string> PostAsync(string url, string json)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(json);
            var r = await client.PostAsync(url, content);
            if (r.IsSuccessStatusCode)
            {
                var s = r.Content.ReadAsStringAsync().Result;
                return s;
            }
            return null;
        }

        public static async Task<string> PutAsync(string url, string json)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(json);
            var r = await client.PutAsync(url, content);
            if (r.IsSuccessStatusCode)
            {
                var s = r.Content.ReadAsStringAsync().Result;
                return s;
            }
            return null;
        }

        public static async Task<string> DeleteAsync(string url)
        {
            HttpClient client = new HttpClient();
            var r = await client.DeleteAsync(url);
            if (r.IsSuccessStatusCode)
            {
                var s = r.Content.ReadAsStringAsync().Result;
                return s;
            }
            return null;
        }
    }
}
