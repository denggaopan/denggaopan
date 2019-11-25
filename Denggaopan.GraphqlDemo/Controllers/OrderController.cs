using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Denggaopan.GraphqlDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Denggaopan.GraphqlDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IDataStore _repo;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IDataStore repo,ILogger<OrderController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<Object> GetAsync()
        {
            var res = await _repo.GetOrdersAsync();
            _logger.LogInformation($"res:{JsonConvert.SerializeObject(res)}");
            return res;
        }
    }
}