using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IDistributedCache _memoryCache;

        public ValuesController(IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //byte[] result = _memoryCache.Get("Terminal:800020002");
            Common.Redis.Helper redisHelper = new Common.Redis.Helper();
            string tt = redisHelper.StringGet("TT:tt");
            string s = _memoryCache.GetString("tt");
            long time = Common.Time.GetUTCTimestamp(DateTime.Now);
            Models.IOT.Instruction ins = Data.Instruction.Get("9755100001");
            //return result;
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
        public string Post()
        {
            string appDefinition =  new StreamReader(Request.Body).ReadToEnd();
            return appDefinition;
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
