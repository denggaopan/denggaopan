using Denggaopan.QrCode.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;

namespace Denggaopan.QrCode
{
    /// <summary>
    /// QRCoder二维码服务
    /// </summary>
    public class QrCodeService : IQrCodeService 
    {
        public QrCodeConfig Config { get; set; }
        public QrCodeService() {
            Config = new QrCodeConfig();
        }
        public QrCodeService(QrCodeConfig config)
        {
            Config = config;
        }

        private QRCodeGenerator.ECCLevel _eccLevel {
            get
            {
                var level = QRCodeGenerator.ECCLevel.Q;
                switch (Config.EccLevel)
                {
                    case ErrorCorrectedLevel.L:
                        level = QRCodeGenerator.ECCLevel.L;
                        break;
                    case ErrorCorrectedLevel.M:
                        level = QRCodeGenerator.ECCLevel.M;
                        break;
                    case ErrorCorrectedLevel.Q:
                        level = QRCodeGenerator.ECCLevel.Q;
                        break;
                    case ErrorCorrectedLevel.H:
                        level = QRCodeGenerator.ECCLevel.H;
                        break;
                }
                return level;
            }
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>Base64 PNG</returns>
        public string RenderBase64(string content)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(content, _eccLevel);
            var qrCode = new Base64QRCode(data);
            return "data:image/png;base64," + qrCode.GetGraphic(Config.Size, Config.DarkColor, Config.LightColor, Config.HasPadding);
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>Svg</returns>
        public string RenderSvg(string content)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(content, _eccLevel);
            var qrCode = new SvgQRCode(data);
            return qrCode.GetGraphic(Config.Size, Config.DarkColor, Config.LightColor, Config.HasPadding);
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>PNG</returns>
        public Stream RenderStream(string content)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(content, _eccLevel);
            var qrCode = new QRCode(data);
            var bitmap = qrCode.GetGraphic(Config.Size, Config.DarkColor, Config.LightColor, Config.HasPadding);
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms;
        }

        public List<Stream> RenderStream(List<string> contents)
        {
            var list = new List<Stream>();
            foreach(var content in contents)
            {
                var stream = RenderStream(content);
                list.Add(stream);
            }
            return list;
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>PNG</returns>
        public byte[] RenderBytes(string content)
        {
            var stream = RenderStream(content);
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            return bytes;
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns>PNG</returns>
        public List<byte[]> RenderBytes(List<string> contents)
        {
            var list = new List<byte[]>();
            foreach (var content in contents)
            {
                var bytes = RenderBytes(content);
                list.Add(bytes);
            }
            return list;
        }

        /// <summary>
        /// 绘制二维码(有边距)
        /// </summary>
        /// <param name="content"></param>
        /// <returns>Bitmap</returns>
        public byte[] RenderBitmapBytes(string content)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(content, _eccLevel);
            var qrCode = new BitmapByteQRCode(data);
            return qrCode.GetGraphic(Config.Size, Config.DarkColor, Config.LightColor);
        }

        /// <summary>
        /// 绘制二维码(有边距,不支持自定义颜色)
        /// </summary>
        /// <param name="content"></param>
        /// <returns>PNG</returns>
        public byte[] RenderPngBytes(string content)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(content, _eccLevel);
            var qrCode = new PngByteQRCode(data);
            return qrCode.GetGraphic(Config.Size);
        }
    }
}
