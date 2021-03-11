using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Denggaopan.K8sConfigMapDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApolloController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ApolloController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("item")]
        public IActionResult GetItem(string key)
        {
            var item = _config[key];
            return Ok(item);
        }

        [HttpGet("array")]
        public IActionResult GetArray(string key)
        {
            var arr = _config.GetSection(key).Get<List<string>>();
            return Ok(arr);
        }
    }
}
