using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Denggaopan.VersioningDemo.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Content("v1-" + HttpContext.GetRequestedApiVersion().ToString());
        }

        [ApiVersion("1.1")]
        [HttpGet]
        public IActionResult Get2()
        {
            return Content("v1.1-" + HttpContext.GetRequestedApiVersion().ToString());
        }
    }
}