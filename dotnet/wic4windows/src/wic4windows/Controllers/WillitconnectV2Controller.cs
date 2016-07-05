using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wic4windows.Model;
using wic4windows.Service;
using Microsoft.Extensions.Logging;

namespace wic4windows.Controllers
{
    [Route("v2/[controller]")]
    public class WillitConnectV2Controller : Controller
    {
        private readonly ILogger<WillitConnectV2Controller> _logger;

        public WillitConnectV2Controller (ILogger<WillitConnectV2Controller> logger)
        {
            _logger = logger;
        }
        // GET values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("Get worked");
            return new string[] { "value1", "value2" };
        }


        // POST api/values
        [HttpPost]
        public JsonResult willitconnect([FromBody]EntryRequest value)
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
