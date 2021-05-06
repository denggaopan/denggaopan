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
        private readonly UidGen _uidgen;
        public SampleController(UidGen uidgen)
        {
            _uidgen = uidgen;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var uid = _uidgen.NextId();
            return Ok(uid);
        }
    }
}
