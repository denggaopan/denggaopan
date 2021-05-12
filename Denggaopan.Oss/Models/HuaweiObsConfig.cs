using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Denggaopan.Oss.Models
{
    public class HuaweiObsConfig
    {
        public string Pk { get; set; }

        public string Secret { get; set; }

        public string Endpoint { get; set; }

        public Dictionary<string,string> BucketDomains { get; set; }

        public string BucketDomainScheme { get; set; }
        public bool IsUsedOriginalUrl { get; set; } = true;
    }
}
