using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.Helpers
{
    /// <summary>
    /// Int前加0扩展
    /// </summary>
    public static class IntExtension
    {

        public static string Uniform(this int v, int length)
        {
            var va = v.ToString();
            var vaLen = va.Length;
            if (vaLen >= length) { return va; }
            for (int i = 0; i < length - vaLen; i++)
            {
                va = "0" + va;
            }
            return va;
        }
    }
}

