using Aliyun.OSS;
using Denggaopan.Oss.Models;
using System;
using System.IO;
using System.Net;

namespace Denggaopan.Oss
{
    public class AliyunObjectStorageService : IObjectStorageService
    {
        private readonly AliyunOssConfig _config;
        public AliyunObjectStorageService(AliyunOssConfig config) 
        {
            _config = config;
        }

        public string PutObject(string bucketName, string key, Stream stream)
        {
            try
            {
                var client = new OssClient(_config.Endpoint, _config.AccessKeyId, _config.AccessKeySecret);
                var res = client.PutObject(bucketName, key, stream);
                if (res.HttpStatusCode == HttpStatusCode.OK)
                {
                    return $"{_config.BucketDomainScheme}://{ _config.BucketDomains[bucketName]}/{key}";
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool DeleteObject(string bucketName, string key)
        {
            try
            {
                var client = new OssClient(_config.Endpoint, _config.AccessKeyId, _config.AccessKeySecret);
                var res = client.DeleteObject(bucketName, key);
                return res.HttpStatusCode == HttpStatusCode.OK || res.HttpStatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
