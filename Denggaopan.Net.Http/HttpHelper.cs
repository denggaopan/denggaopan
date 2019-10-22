using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Denggaopan.Net.Http
{
    public class HttpHelper
    {
        public static async Task<string> GetAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var r = await client.GetAsync(url);
                if (r.IsSuccessStatusCode)
                {
                    var s = r.Content.ReadAsStringAsync().Result;
                    return s;
                }
                return null;
            }
        }

        public static async Task<string> GetAsync(string url, Dictionary<string, string> headers)
        {
            using (HttpClient client = new HttpClient())
            {

                if (headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var r = await client.GetAsync(url);
                if (r.IsSuccessStatusCode)
                {
                    var s = r.Content.ReadAsStringAsync().Result;
                    return s;
                }
                return null;
            }
        }

        public static async Task<string> PostAsync(string url, string json)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(json);
                var r = await client.PostAsync(url, content);
                if (r.IsSuccessStatusCode)
                {
                    var s = r.Content.ReadAsStringAsync().Result;
                    return s;
                }
                return null;
            }
        }

        public static async Task<string> PostAsync(string url, Dictionary<string, string> headers, string json)
        {
            using (HttpClient client = new HttpClient())
            {

                if (headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var content = new StringContent(json);
                var r = await client.PostAsync(url, content);
                if (r.IsSuccessStatusCode)
                {
                    var s = r.Content.ReadAsStringAsync().Result;
                    return s;
                }
                return null;
            }
        }

        public static async Task<string> PutAsync(string url, string json)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(json);
                var r = await client.PutAsync(url, content);
                if (r.IsSuccessStatusCode)
                {
                    var s = r.Content.ReadAsStringAsync().Result;
                    return s;
                }
                return null;
            }
        }

        public static async Task<string> PutAsync(string url, Dictionary<string, string> headers, string json)
        {
            using (HttpClient client = new HttpClient())
            {

                if (headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var content = new StringContent(json);
                var r = await client.PutAsync(url, content);
                if (r.IsSuccessStatusCode)
                {
                    var s = r.Content.ReadAsStringAsync().Result;
                    return s;
                }
                return null;
            }
        }

        public static async Task<string> DeleteAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var r = await client.DeleteAsync(url);
                if (r.IsSuccessStatusCode)
                {
                    var s = r.Content.ReadAsStringAsync().Result;
                    return s;
                }
                return null;
            }
        }

        public static async Task<string> DeleteAsync(string url, Dictionary<string, string> headers)
        {
            using (HttpClient client = new HttpClient())
            {

                if (headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

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
}
