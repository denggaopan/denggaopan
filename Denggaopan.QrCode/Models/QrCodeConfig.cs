using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.QrCode.Models
{
    public class QrCodeConfig
    {
        /// <summary>
        /// 二维码模块点像素
        /// </summary>
        public int Size { get; set; } = 6;

        /// <summary>
        /// 纠错级别
        /// </summary>
        public ErrorCorrectedLevel EccLevel { get; set; } = ErrorCorrectedLevel.Q;

        /// <summary>
        /// 二维码模块点颜色
        /// </summary>
        public string DarkColor { get; set; } = "#000000";

        /// <summary>
        /// 二维码模块点颜色
        /// </summary>
        public string LightColor { get; set; } = "#FFFFFF";

        /// <summary>
        /// 有否图片边距
        /// </summary>
        public bool HasPadding { get; set; } = false;
    }
}
