using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Denggaopan.WebApiClient.AService.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Denggaopan.WebApiClient.BService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IAServiceApi _a;
        public TestController(IAServiceApi a)
        {
            _a = a;
        }
        public async Task<string> GetAsync()
        {
            return await _a.CheckHealthAsync();
        }
    }
}
