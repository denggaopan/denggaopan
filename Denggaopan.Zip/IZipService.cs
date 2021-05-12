using System;
using System.Collections.Generic;
using System.IO;

namespace Denggaopan.Zip
{
    public interface IZipService
    {
        void ZipCompress(List<string> sourceFileLists, string descFile, int compression);

        void ZipCompressDir(string dir, string zipFileName, bool recurse = true, string filter = null);

        void ZipDeCompressDir(string zipFileName, string dir, string filter = null);

        void ZipDeCompress(string zipFileName, string dir);

        string ZipCompress(string text);

        string ZipDeCompress(string text);

        byte[] ZipCompress(byte[] data, bool isClearData = true);

        byte[] ZipDeCompress(byte[] data, bool isClearData = true);

        void UpdateZipInMemory(Stream zipStream, Stream entryStream, String entryName);

        string GZipCompress(string text);

        string GZipDeCompress(string text);

        byte[] GZipCompress(byte[] data, bool isClearData = true);

        byte[] GZipDeCompress(byte[] data, bool isClearData = true);

        string TarCompress(string text);

        string TarDeCompress(string text);

        byte[] TarCompress(byte[] data, bool isClearData = true);

        byte[] TarDeCompress(byte[] data, bool isClearData = true);

        string BZipCompress(string text);

        string BZipDeCompress(string text);

        byte[] BZipCompress(byte[] data, bool isClearData = true);

        byte[] BZipDeCompress(byte[] data, bool isClearData = true);
    }
}
