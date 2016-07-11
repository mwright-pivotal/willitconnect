using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wic4windows.Model;
using Microsoft.Extensions.Logging;
using wic4windows_v2.Services;

namespace wic4windows_v2.Controllers
{
    [Route("v2/[controller]")]
    public class WillitConnectController : Controller
    {
        private readonly ILogger<WillitConnectController> _logger;

        public WillitConnectController(ILogger<WillitConnectController> logger)
        {
            _logger = logger;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]EntryRequest value)
        {
            CheckedEntry entry = new CheckedEntry(value.Target);
            EntryChecker checker = new EntryChecker(_logger);
            checker.check(entry);
            return new JsonResult(entry);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
