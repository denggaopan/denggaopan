using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.QrCode.Models
{
    /// <summary>
    /// 容错级别
    /// </summary>
    public enum ErrorCorrectedLevel
    {
        /// <summary>
        /// 可以纠正最大7%的错误
        /// </summary>
        L,
        /// <summary>
        /// 可以纠正最大15%的错误
        /// </summary>
        M,
        /// <summary>
        /// 可以纠正最大25%的错误
        /// </summary>
        Q,
        /// <summary>
        /// 可以纠正最大30%的错误
        /// </summary>
        H
    }
}
