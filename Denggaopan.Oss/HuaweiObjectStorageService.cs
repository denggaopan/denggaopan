using Denggaopan.Oss.Models;
using OBS;
using OBS.Model;
using System;
using System.IO;
using System.Net;

namespace Denggaopan.Oss
{
    public class HuaweiObjectStorageService : IObjectStorageService
    {
        private readonly HuaweiObsConfig _config;
        public HuaweiObjectStorageService(HuaweiObsConfig config)
        {
            _config = config;
        }

        public string PutObject(string bucketName, string key, Stream fileStream)
        {
            try
            {
                ObsClient client = new ObsClient(_config.Pk, _config.Secret, _config.Endpoint);
                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = key,
                    InputStream = fileStream,
                };
                var res = client.PutObject(request);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    if (_config.IsUsedOriginalUrl)
                    {
                        return res.ObjectUrl;//使用华为云原始链接
                    }
                    return $"{_config.BucketDomainScheme}://{_config.BucketDomains[bucketName]}/{key}";
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
                ObsClient client = new ObsClient(_config.Pk, _config.Secret, _config.Endpoint);
                DeleteObjectRequest request = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = key
                };
                var res = client.DeleteObject(request);
                return res.StatusCode == HttpStatusCode.OK || res.StatusCode == HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
