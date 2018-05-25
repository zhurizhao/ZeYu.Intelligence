using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ZeYu.Intelligence.WebAPI.Business
{
    public class TCP : AbstractCommand
    {
        public override JObject Send(long terminalID, string uniCode, string apiURL, string accessSecret = null)
        {
            Models.IOT.Command cmd = this.Create(terminalID, uniCode);
            JObject requestBody = new JObject();
            requestBody.Add("TerminalID", terminalID);
            requestBody.Add("HexPacket", cmd.HexPacket);

            string key = "Terminal:"+ terminalID;
            string transeURL = new Common.Redis.Helper().HashGet(key,"TranseURL");
            string response = Common.HttpClientHelper.HttpPost(transeURL, requestBody.ToString(Newtonsoft.Json.Formatting.None));

            return JObject.Parse(response);
        }
    }
}
