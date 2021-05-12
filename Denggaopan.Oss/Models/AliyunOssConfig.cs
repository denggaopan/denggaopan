using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.Oss.Models
{
    public class AliyunOssConfig
    {
        public string AccessKeyId { get; set; }

        public string AccessKeySecret { get; set; }

        public string Endpoint { get; set; }

        public Dictionary<string,string> BucketDomains { get; set; }

        public string BucketDomainScheme { get; set; }
    }
}
