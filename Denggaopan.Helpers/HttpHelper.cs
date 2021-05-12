using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Denggaopan.Helpers
{
    public class HttpHelper
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if(timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    client.Timeout = TimeSpan.FromMinutes(1);
                    var r = await client.GetAsync(url);
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static string Get(string url, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    client.Timeout = TimeSpan.FromMinutes(1);
                    var r = client.GetAsync(url).Result;
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Dictionary<string, string> headers, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
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
            catch { }
            return null;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> headers, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    if (headers.Count > 0)
                    {
                        foreach (var header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                    var r = client.GetAsync(url).Result;
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }

                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string json, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
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
            catch { }
            return null;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static string Post(string url, string json, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    var content = new StringContent(json);
                    var r = client.PostAsync(url, content).Result;
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="json"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, Dictionary<string, string> headers, string json, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
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
            catch { }
            return null;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="json"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> headers, string json, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    if (headers.Count > 0)
                    {
                        foreach (var header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }

                    var content = new StringContent(json);
                    var r = client.PostAsync(url, content).Result;
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static async Task<string> PutAsync(string url, string json, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
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
            catch { }
            return null;
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static string Put(string url, string json, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    var content = new StringContent(json);
                    var r = client.PutAsync(url, content).Result;
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="json"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static async Task<string> PutAsync(string url, Dictionary<string, string> headers, string json, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
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
            catch { }
            return null;
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="json"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static string Put(string url, Dictionary<string, string> headers, string json, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    if (headers.Count > 0)
                    {
                        foreach (var header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }

                    var content = new StringContent(json);
                    var r = client.PutAsync(url, content).Result;
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static async Task<string> DeleteAsync(string url, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    var r = await client.DeleteAsync(url);
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static string Delete(string url, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    var r = client.DeleteAsync(url).Result;
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="timeout">请求超时（秒）</param>
        /// <returns></returns>
        public static string Delete(string url, Dictionary<string, string> headers, int timeout = 15)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (timeout <= 0)
                    {
                        timeout = 15;
                    }
                    client.Timeout = TimeSpan.FromMilliseconds(timeout * 1000);
                    if (headers.Count > 0)
                    {
                        foreach (var header in headers)
                        {
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }

                    var r = client.DeleteAsync(url).Result;
                    if (r.IsSuccessStatusCode)
                    {
                        var s = r.Content.ReadAsStringAsync().Result;
                        return s;
                    }
                    return null;
                }
            }
            catch { }
            return null;
        }
    }
}
