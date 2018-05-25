using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    [Produces("application/json")]
    //[Route("api/Instruction")]
    public class InstructionController : Controller
    {
        private IDistributedCache _memoryCache;

        public InstructionController(IDistributedCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // GET api/Instruction
        [HttpGet]
        public  string Get()
        {
            string result = _memoryCache.GetString("Terminal:800020002");
            return result;
        }

        // GET api/values/5
        [HttpGet("{key}")]
        public string Get(string key)
        {
            return _memoryCache.GetString(key);
        }

        // GET api/values/5
        [HttpGet("{key}")]
        public string HGet(string key,string[] field )
        {
            return _memoryCache.GetString(key);
        }

        // GET api/values/5
        [Route("api/[controller]/Send")]
        [HttpPost]
        public string Send([FromBody] JObject jObject)
        {
            
            uint terminalID = (uint)jObject["TerminalID"];
            string uniCode = jObject["UniCode"].ToString();
            bool Top = false;
            if (jObject["Top"] != null)
            {
                Top = (bool)jObject["Top"];
            }

            if (Top)
            {
                //转发该参数
                return "该功能还未实现！";
            }

            Common.Redis.Helper redis = new Common.Redis.Helper();
            string key = "Terminal:" + terminalID;
            string accessSecret = redis.HashGet("Access", "TranseURL");
            string transeURL = redis.HashGet(key, "TranseURL");
            string tlp = "TCP";//默认TCP
            string cacheTLP = redis.HashGet(key, "TLP");
            if (!string.IsNullOrWhiteSpace(cacheTLP))
            {
                tlp = cacheTLP;
            }

            string instanceName = "ZeYu.Intelligence.WebAPI.Business." + tlp;
            Business.AbstractCommand instance =  Assembly.Load("ZeYu.Intelligence.WebAPI").CreateInstance(instanceName) as Business.AbstractCommand;

            string response = instance.Send(terminalID, uniCode, transeURL, null).ToString();
            //Models.IOT.Command cmd = instance.Create (terminalID, uniCode); //,transeURL,accessSecret
            //string postCmdData = JsonConvert.SerializeObject(cmd);

            //string response = Common.HttpClientHelper.Request(transeURL, System.Text.Encoding.UTF8 ,postCmdData);


            return response;
        }

    }
}