using System;
using System.Diagnostics;

namespace Denggaopan.TinyUrl
{
    /// <summary>
    /// TinyUrlHelper
    /// </summary>
    public class TinyUrlHelper
    {
        private const string SEQ1 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string SEQ2 = "7vqL64ETrtkiB1NV3wbCDAGQOxcplURus0XSP95yWZHaeY8oJj2fKFmnhdMIgz";

        /// <summary>
        /// 10进制转换为62进制（混淆版）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSorting"></param>
        /// <returns></returns>
        public static string Parse(long id, bool isSorting = false)
        {
            return Mixup(Convert(id,isSorting));
        }

        /// <summary>
        /// 将62进制转为10进制（混淆版）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isSorting"></param>
        /// <returns></returns>
        public static long Parse(string key, bool isSorting = false)
        {
            return Convert(UnMixup(key), isSorting);
        }

        /// <summary>
        /// 10进制转换为62进制
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSorting"></param>
        /// <returns></returns>
        public static string Convert(long id, bool isSorting = false)
        {
            var Seq = isSorting ? SEQ1 : SEQ2;

            if (id < 62)
            {
                return Seq[(int)id].ToString();
            }
            int y = (int)(id % 62);
            long x = (long)(id / 62);

            return Convert(x,isSorting) + Seq[y];
        }

        /// <summary>
        /// 将62进制转为10进制
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isSorting"></param>
        /// <returns></returns>
        public static long Convert(string key, bool isSorting = false)
        {
            var Seq = isSorting ? SEQ1 : SEQ2;
            long v = 0;
            int Len = key.Length;
            for (int i = Len - 1; i >= 0; i--)
            {
                int t = Seq.IndexOf(key[i]);
                double s = (Len - i) - 1;
                long m = (long)(Math.Pow(62, s) * t);
                v += m;
            }
            return v;
        }

        /// <summary>
        /// 混淆id为字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Mixup(string Key)
        {
            int s = 0;
            foreach (char c in Key)
            {
                s += (int)c;
            }
            int Len = Key.Length;
            int x = (s % Len);
            char[] arr = Key.ToCharArray();
            char[] newarr = new char[arr.Length];
            Array.Copy(arr, x, newarr, 0, Len - x);
            Array.Copy(arr, 0, newarr, Len - x, x);
            string NewKey = "";
            foreach (char c in newarr)
            {
                NewKey += c;
            }
            return NewKey;
        }

        /// <summary>
        /// 解开混淆字符串
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string UnMixup(string Key)
        {
            int s = 0;
            foreach (char c in Key)
            {
                s += (int)c;
            }
            int Len = Key.Length;
            int x = (s % Len);
            x = Len - x;
            char[] arr = Key.ToCharArray();
            char[] newarr = new char[arr.Length];
            Array.Copy(arr, x, newarr, 0, Len - x);
            Array.Copy(arr, 0, newarr, Len - x, x);
            string NewKey = "";
            foreach (char c in newarr)
            {
                NewKey += c;
            }
            return NewKey;
        }

        /// <summary>
        /// 生成随机的0-9a-zA-Z字符串
        /// </summary>
        /// <returns></returns>
        public static string GenerateKeys()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string[] Chars = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z".Split(',');
            int SeekSeek = unchecked((int)DateTime.Now.Ticks);
            Random SeekRand = new Random(SeekSeek);
            for (int i = 0; i < 100000; i++)
            {
                int r = SeekRand.Next(1, Chars.Length);
                string f = Chars[0];
                Chars[0] = Chars[r - 1];
                Chars[r - 1] = f;
            }
            sw.Stop();
            var ms1 = sw.ElapsedMilliseconds;
            Console.WriteLine($"GenerateKeys2 used time:{ms1}");
            return string.Join("", Chars);
        }

        public static string GenerateKeys2()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string[] Chars = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z".Split(',');
            Random SeekRand = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < Chars.Length; i++)
            {
                int r = SeekRand.Next(0, Chars.Length-1);
                string f = Chars[i];
                Chars[i] = Chars[r];
                Chars[r] = f;
            }
            sw.Stop();
            var ms2 = sw.ElapsedMilliseconds;
            Console.WriteLine($"GenerateKeys2 used time:{ms2}");
            return string.Join("", Chars);
        }

        /// <summary>
        /// 返回随机递增步长
        /// </summary>
        /// <param name="SeekRand"></param>
        /// <returns></returns>
        public static int GetRnd(int max = 10)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            var step = rnd.Next(1, max + 1);
            return step;
        }


    }
}
