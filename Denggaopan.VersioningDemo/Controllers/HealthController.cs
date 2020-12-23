using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Denggaopan.VersioningDemo.Controllers
{
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [ApiVersion("1.0")]
        [HttpGet]
        public IActionResult Get(ApiVersion apiVersion)
        {
            return Content("v1-" + HttpContext.GetRequestedApiVersion().ToString() + "-" + apiVersion);
        }

        [ApiVersion("1.1")]
        [HttpGet]
        public IActionResult Get2(ApiVersion apiVersion)
        {
            return Content("v1.1-" + HttpContext.GetRequestedApiVersion().ToString() + "-" + apiVersion);
        }
    }
}