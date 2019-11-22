using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public class GraphqlRequest
    {
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
