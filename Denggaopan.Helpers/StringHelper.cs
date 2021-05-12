using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.Helpers
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 将字符串按照指定长度以空格隔开
        /// 例如：1234567890
        /// 分割成：1234 5678 90
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="length">分隔长度</param>
        /// <param name="spiter">分隔符号，默认是空格</param>
        /// <returns></returns>
        public static string Separate(this string strValue, int length = 4, string spiter = " ")
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return "";
            }
            var ts = strValue.Trim().Replace(" ", "");
            if (length <= 0)
            {
                return ts;
            }
            var ret = new StringBuilder();
            for (var i = 0; i < ts.Length; i++)
            {
                ret.Append(ts[i].ToString());
                if (i > 0 && (i+1) % length == 0)
                {
                    ret.Append(spiter);
                }
            }
            return ret.ToString();
        }
        /// <summary>
        /// 为字符串增加遮罩
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string Mask(this string strValue, int start, int end, string mask = "*")
        {
            var strArray = strValue.ToArray();
            var length = strArray.Length;
            var sb = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                sb.Append((i >= start && i <= end) ? mask : strArray[i].ToString());
            }
            return sb.ToString();
        }
        public static DateTime? ToDate(this string strValue, string format)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return null;
            }
            var valueDate = DateTime.Now;
            if (DateTime.TryParseExact(strValue, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out valueDate))
            {
                return valueDate;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string strValue, string format)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return null;
            }
            var valueDate = DateTime.Now;
            if (DateTime.TryParseExact(strValue, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out valueDate))
            {
                return valueDate;

            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="format">
        /// dd/MM/yyyy
        /// yyyy/MM/dd
        /// </param>
        /// <returns></returns>
        public static DateTime? ToDate(this string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return null;
            }
            var formats = new string[] { "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd", "yyyy.MM.dd", "yyyy年MM月dd日" };
            var valueDate = DateTime.Now;
            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(strValue, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out valueDate))
                {
                    return valueDate;
                }
            }
            if (DateTime.TryParse(strValue, out valueDate))
            {
                return valueDate;
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return null;
            }
            var formats = new string[] { "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm",
                                         "dd/MM/yyyy HH:mm:ss",  "dd/MM/yyyy HH:mm",
                                         "MM/dd/yyyy  HH:mm:ss", "MM/dd/yyyy  HH:mm",
                                         "yyyy/MM/dd  HH:mm:ss", "yyyy/MM/dd  HH:mm",
                                         "yyyy.MM.dd  HH:mm:ss", "yyyy.MM.dd  HH:mm",
                                         "yyyy年MM月dd日 HH:mm:ss",  "yyyy年MM月dd日 HH:mm" };
            var valueDate = DateTime.Now;
            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(strValue, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out valueDate))
                {
                    return valueDate;

                }
            }
            if (DateTime.TryParse(strValue, out valueDate))
            {
                return valueDate;
            }
            return null;
        }

        public static string GetNonceStr(int length)
        {
            string[] Chars = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z".Split(',');
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                Random r = new Random(Guid.NewGuid().GetHashCode());
                var index = r.Next(Chars.Length);
                sb.Append(Chars[index]);
            }

            return sb.ToString();
        }

        public static string Md5(string str)
        {
            return BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "");
        }

    }
}

