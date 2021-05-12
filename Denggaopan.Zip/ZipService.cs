using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using Denggaopan.Zip.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Denggaopan.Zip
{
    public class ZipService : IZipService
    {
        private const int BUFFER_LENGTH = 2048;
        #region Zip  
        /// <summary>
        /// Zip文件压缩
        /// ZipOutputStream：相当于一个压缩包；
        /// ZipEntry：相当于压缩包里的一个文件；
        /// 以上两个类是SharpZipLib的主类。
        /// </summary>
        /// <param name="sourceFileLists"></param>
        /// <param name="descFile">压缩文件保存的目录</param>
        /// <param name="compression">压缩级别</param>
        public void ZipCompress(List<string> sourceFileLists, string descFile, int compression)
        {
            if (compression < 0 || compression > 9)
            {
                throw new ArgumentException("错误的压缩级别");
            }
            if (!Directory.Exists(new FileInfo(descFile).Directory.ToString()))
            {
                throw new ArgumentException("保存目录不存在");
            }
            foreach (string c in sourceFileLists)
            {
                if (!File.Exists(c))
                {
                    throw new ArgumentException(string.Format("文件{0} 不存在！", c));
                }
            }
            Crc32 crc32 = new Crc32();
            using (ZipOutputStream stream = new ZipOutputStream(File.Create(descFile)))
            {
                stream.SetLevel(compression);
                ZipEntry entry;
                for (int i = 0; i < sourceFileLists.Count; i++)
                {
                    entry = new ZipEntry(Path.GetFileName(sourceFileLists[i]));
                    entry.DateTime = DateTime.Now;
                    using (FileStream fs = File.OpenRead(sourceFileLists[i]))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        entry.Size = fs.Length;
                        crc32.Reset();
                        crc32.Update(buffer);
                        entry.Crc = crc32.Value;
                        stream.PutNextEntry(entry);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    stream.CloseEntry();
                }

            }
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="directory">文件夹</param>
        /// <param name="zipFileName">目标文件</param>
        /// <param name="recurse">通过目录结构递归包含所有文件</param>
        /// <param name="filter">过滤文件</param>
        public void ZipCompressDir(string dir, string zipFileName, bool recurse = true, string filter = null)
        {
            FastZip fastZip = new FastZip();
            fastZip.CreateZip(zipFileName, dir, recurse, filter);
        }

        /// <summary>
        /// 解压到文件夹
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <param name="dir"></param>
        /// <param name="filter"></param>
        public void ZipDeCompressDir(string zipFileName, string dir, string filter = null)
        {
            FastZip fastZip = new FastZip();
            fastZip.ExtractZip(zipFileName, dir, filter);
        }

        /// <summary>
        /// unZip文件解压
        /// </summary>
        /// <param name="zipFileName">要解压的文件</param>
        /// <param name="dir">要解压到的目录</param>
        [Obsolete]
        public void ZipDeCompress(string zipFileName, string dir)
        {
            if (!File.Exists(zipFileName))
            {
                throw new ArgumentException("要解压的文件不存在。");
            }
            if (!Directory.Exists(dir))
            {
                try
                {
                    Directory.CreateDirectory(dir);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFileName)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (fileName != string.Empty)
                    {
                        using (FileStream streamWriter = File.Create(dir + @"\" + theEntry.Name))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 字符串压缩
        /// </summary>
        /// <param name="text">待压缩的字符串</param>
        /// <returns>已压缩的字符串</returns>
        public string ZipCompress(string text)
        {
            string result = string.Empty;
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] dData = ZipCompress(data);
            result = Convert.ToBase64String(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }
        /// <summary>
        /// 字符串解压
        /// </summary>
        /// <param name="text">待解压的字符串</param>
        /// <returns>已解压的字符串</returns>
        public string ZipDeCompress(string text)
        {
            string result = string.Empty;
            byte[] data = Convert.FromBase64String(text);
            byte[] dData = ZipDeCompress(data);
            result = Encoding.UTF8.GetString(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }
        /// <summary>
        /// 字节数组压缩
        /// </summary>
        /// <param name="data">待压缩的字节数组</param>
        /// <param name="isClearData">压缩完成后，是否清除待压缩字节数组里面的内容</param>
        /// <returns>已压缩的字节数组</returns>
        public byte[] ZipCompress(byte[] data, bool isClearData = true)
        {
            byte[] bytes = null;
            Deflater f = new Deflater(Deflater.BEST_COMPRESSION);
            f.SetInput(data);
            f.Finish();
            int count = 0;
            using (MemoryStream o = new MemoryStream(data.Length))
            {
                byte[] buffer = new byte[BUFFER_LENGTH];
                while (!f.IsFinished)
                {
                    count = f.Deflate(buffer);
                    o.Write(buffer, 0, count);
                }
                bytes = o.ToArray();
            }
            if (isClearData)
            {
                Array.Clear(data, 0, data.Length);
            }
            return bytes;
        }
        /// <summary>
        /// 字节数组解压缩
        /// </summary>
        /// <param name="data">待解压缩的字节数组</param>
        /// <param name="isClearData">解压缩完成后，是否清除待解压缩字节数组里面的内容</param>
        /// <returns>已解压的字节数组</returns>
        public byte[] ZipDeCompress(byte[] data, bool isClearData = true)
        {
            byte[] bytes = null;
            Inflater f = new Inflater();
            f.SetInput(data);
            int count = 0;
            using (MemoryStream o = new MemoryStream(data.Length))
            {
                byte[] buffer = new byte[BUFFER_LENGTH];
                while (!f.IsFinished)
                {
                    count = f.Inflate(buffer);
                    o.Write(buffer, 0, count);
                }
                bytes = o.ToArray();
            }
            if (isClearData)
            {
                Array.Clear(data, 0, data.Length);
            }
            return bytes;
        }

        public void UpdateZipInMemory(Stream zipStream, Stream entryStream, String entryName)
        {

            // The zipStream is expected to contain the complete zipfile to be updated
            ZipFile zipFile = new ZipFile(zipStream);

            zipFile.BeginUpdate();

            // To use the entryStream as a file to be added to the zip,
            // we need to put it into an implementation of IStaticDataSource.
            CustomStaticDataSource sds = new CustomStaticDataSource();
            sds.SetStream(entryStream);

            // If an entry of the same name already exists, it will be overwritten; otherwise added.
            zipFile.Add(sds, entryName);

            // Both CommitUpdate and Close must be called.
            zipFile.CommitUpdate();
            // Set this so that Close does not close the memorystream
            zipFile.IsStreamOwner = false;
            zipFile.Close();

            // Reposition to the start for the convenience of the caller.
            zipStream.Position = 0;
        }


        #endregion

        #region GZip
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="text">待压缩的字符串组</param>
        /// <returns>已压缩的字符串</returns>
        public string GZipCompress(string text)
        {
            string result = string.Empty;
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] cData = GZipCompress(data);
            result = Convert.ToBase64String(cData);
            Array.Clear(cData, 0, cData.Length);
            return result;
        }
        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="text">待解压缩的字符串</param>
        /// <returns>已解压缩的字符串</returns>
        public string GZipDeCompress(string text)
        {
            string result = string.Empty;
            byte[] data = Convert.FromBase64String(text);
            byte[] cData = GZipDeCompress(data);
            result = Encoding.UTF8.GetString(cData);
            Array.Clear(cData, 0, cData.Length);
            return result;
        }
        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="data">待压缩的字节数组</param>
        /// <param name="isClearData">压缩完成后，是否清除待压缩字节数组里面的内容</param>
        /// <returns>已压缩的字节数组</returns>
        public byte[] GZipCompress(byte[] data, bool isClearData = true)
        {
            byte[] bytes = null;
            try
            {
                using (MemoryStream o = new MemoryStream())
                {
                    using (Stream s = new GZipOutputStream(o))
                    {
                        s.Write(data, 0, data.Length);
                        s.Flush();
                    }
                    bytes = o.ToArray();
                }
            }
            catch (SharpZipBaseException)
            {
            }
            catch (IndexOutOfRangeException)
            {
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return bytes;
        }

        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="data">待解压缩的字节数组</param>
        /// <param name="isClearData">解压缩完成后，是否清除待解压缩字节数组里面的内容</param>
        /// <returns>已解压的字节数组</returns>
        public byte[] GZipDeCompress(byte[] data, bool isClearData = true)
        {
            byte[] bytes = null;
            try
            {
                using (MemoryStream o = new MemoryStream())
                {
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        using (Stream s = new GZipInputStream(ms))
                        {
                            s.Flush();
                            int size = 0;
                            byte[] buffer = new byte[BUFFER_LENGTH];
                            while ((size = s.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                o.Write(buffer, 0, size);
                            }
                        }
                    }
                    bytes = o.ToArray();
                }
            }
            catch (SharpZipBaseException)
            {
            }
            catch (IndexOutOfRangeException)
            {
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return bytes;
        }
        #endregion

        #region Tar
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="text">待压缩的字符串组</param>
        /// <returns>已压缩的字符串</returns>
        public string TarCompress(string text)
        {
            string result = null;
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] dData = TarCompress(data);
            result = Convert.ToBase64String(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }
        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="text">待解压缩的字符串</param>
        /// <returns>已解压的字符串</returns>
        public string TarDeCompress(string text)
        {
            string result = null;
            byte[] data = Convert.FromBase64String(text);
            byte[] dData = TarDeCompress(data);
            result = Encoding.UTF8.GetString(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }
        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="data">待压缩的字节数组</param>
        /// <param name="isClearData">压缩完成后，是否清除待压缩字节数组里面的内容</param>
        /// <returns>已压缩的字节数组</returns>
        public byte[] TarCompress(byte[] data, bool isClearData = true)
        {
            byte[] bytes = null;
            using (MemoryStream o = new MemoryStream())
            {
                using (Stream s = new TarOutputStream(o))
                {
                    s.Write(data, 0, data.Length);
                    s.Flush();
                }
                bytes = o.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return bytes;
        }
        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="data">待解压缩的字节数组</param>
        /// <param name="isClearData">解压缩完成后，是否清除待解压缩字节数组里面的内容</param>
        /// <returns>已解压的字节数组</returns>
        public byte[] TarDeCompress(byte[] data, bool isClearData = true)
        {
            byte[] bytes = null;
            using (MemoryStream o = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (Stream s = new TarInputStream(ms))
                    {
                        s.Flush();
                        int size = 0;
                        byte[] buffer = new byte[BUFFER_LENGTH];
                        while ((size = s.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            o.Write(buffer, 0, size);
                        }
                    }
                }
                bytes = o.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return bytes;
        }
        #endregion

        #region BZip
        /// <summary>
        /// 压缩字符串
        /// </summary>
        /// <param name="text">待压缩的字符串组</param>
        /// <returns>已压缩的字符串</returns>
        public string BZipCompress(string text)
        {
            string result = null;
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] dData = BZipCompress(data);
            result = Convert.ToBase64String(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }
        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="text">待解压缩的字符串</param>
        /// <returns>已解压的字符串</returns>
        public string BZipDeCompress(string text)
        {
            string result = null;
            byte[] data = Convert.FromBase64String(text);
            byte[] dData = BZipDeCompress(data);
            result = Encoding.UTF8.GetString(dData);
            Array.Clear(dData, 0, dData.Length);
            return result;
        }
        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="data">待压缩的字节数组</param>
        /// <param name="isClearData">压缩完成后，是否清除待压缩字节数组里面的内容</param>
        /// <returns>已压缩的字节数组</returns>
        public byte[] BZipCompress(byte[] data, bool isClearData = true)
        {
            byte[] bytes = null;
            using (MemoryStream o = new MemoryStream())
            {
                using (Stream s = new BZip2OutputStream(o))
                {
                    s.Write(data, 0, data.Length);
                    s.Flush();
                }
                bytes = o.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return bytes;
        }
        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="data">待解压缩的字节数组</param>
        /// <param name="isClearData">解压缩完成后，是否清除待解压缩字节数组里面的内容</param>
        /// <returns>已解压的字节数组</returns>
        public byte[] BZipDeCompress(byte[] data, bool isClearData = true)
        {
            byte[] bytes = null;
            using (MemoryStream o = new MemoryStream())
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (Stream s = new BZip2InputStream(ms))
                    {
                        s.Flush();
                        int size = 0;
                        byte[] buffer = new byte[BUFFER_LENGTH];
                        while ((size = s.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            o.Write(buffer, 0, size);
                        }
                    }
                }
                bytes = o.ToArray();
            }
            if (isClearData)
                Array.Clear(data, 0, data.Length);
            return bytes;
        }
        #endregion
    }
}
