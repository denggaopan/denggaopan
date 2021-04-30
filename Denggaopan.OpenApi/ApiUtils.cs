using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Denggaopan.OpenApi
{
    public class ApiUtils
    {
        public static string Sign(Dictionary<string, string> Parameters, string AppSecret)
        {
            var myParameters = Parameters.OrderBy(c => c.Key);
            StringBuilder data = new StringBuilder();
            foreach (var par in myParameters)
            {
                if (par.Key.ToUpper() != "SIGN" && !string.IsNullOrEmpty(par.Value))
                {
                    data.AppendFormat("{0}={1}&", par.Key, par.Value);
                }
            }
            data.Append(AppSecret);
            var sing = Md5(data.ToString().ToUpper()).ToUpper();
            return sing;
        }

        public static Dictionary<string, object> Obj2Dic<T>(T obj)
        {
            var props = typeof(T).GetProperties();
            var dic = new Dictionary<string, object>();
            foreach(var prop in props)
            {
                var propTypeName = prop.GetType().Name;
                //todo：check typename
                dic.Add(prop.Name, prop.GetValue(obj));
            }
            return dic;
        }

        public static string Md5(string str)
        {
            return BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "");
        }

        public static long GetTimestamp()
        {
            return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public static string GetNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
