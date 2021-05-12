using System;
using System.IO;

namespace Denggaopan.Oss
{
    public interface IObjectStorageService
    {
        /// <summary>
        /// 上传对象
        /// </summary>
        /// <param name="bucketName">桶名称</param>
        /// <param name="key">Key</param>
        /// <param name="fileStream">文件流</param>
        /// <returns></returns>
        string PutObject(string bucketName, string key, Stream fileStream);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="bucketName">桶名称</param>
        /// <param name="key">Key</param>
        /// <returns></returns>
        bool DeleteObject(string bucketName, string key);
    }
}
