using System;
using System.Collections.Generic;
using System.IO;
using Denggaopan.QrCode.Models;

namespace Denggaopan.QrCode
{
    public interface IQrCodeService
    {
        QrCodeConfig Config { get; set; }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>PNG</returns>
        Stream RenderStream(string content);

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>PNG</returns>
        List<Stream> RenderStream(List<string> contents);

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>PNG</returns>
        byte[] RenderBytes(string content);

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>PNG</returns>
        List<byte[]> RenderBytes(List<string> contents);

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>PNG Base64</returns>
        string RenderBase64(string content);

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>SVG</returns>
        string RenderSvg(string content);

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>Bitmap</returns>
        byte[] RenderBitmapBytes(string content);

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>PNG</returns>
        byte[] RenderPngBytes(string content);
    }
}
