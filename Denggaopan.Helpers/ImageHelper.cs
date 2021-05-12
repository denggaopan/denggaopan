using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.Helpers
{
    public class ImageHelper
    {

        public static Stream ConvertBase64ToStream(string base64String)
        {
            if (!base64String.StartsWith("data:image"))
            {
                throw new BadImageFormatException("图片数据错误");
            }

            var str =base64String
                .Replace("data:image/png;base64,", "")
                .Replace("data:image/jpg;base64,", "")
                .Replace("data:image/jpeg;base64,", "")
                .Replace("data:image/bmp;base64,", "")
                .Replace("data:image/gif;base64,", "");
            byte[] imageBytes = Convert.FromBase64String(str);
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            return ms;
        }

        public static string getImageType(string base64String)
        {
            var type = base64String.Substring(11, 4).Replace(";","");
            type = type == "jpeg" ? "jpg" : type;
            return type;
        }
    }
}
