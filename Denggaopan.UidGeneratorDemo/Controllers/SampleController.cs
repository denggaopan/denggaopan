using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Denggaopan.UidGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Denggaopan.UidGeneratorDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IdGenerator _idgen;
        public SampleController(IdGenerator idgen)
        {
            _idgen = idgen;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var uid = _idgen.NextId();
            return Ok(uid);
        }
    }
}
