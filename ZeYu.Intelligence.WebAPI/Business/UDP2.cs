using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ZeYu.Intelligence.WebAPI.Business
{
    public class UDP2 : AbstractCommand
    {
        public override JObject Send(long terminalID, string uniCode, string apiURL, string accessSecret = null)
        {
            apiURL = "http://xinyun.luyuan.cn/api.ashx?cla=BAILI.Modules.Systems.SessionAction&fn=create&code=JSON";
            string response = Common.HttpClientHelper.HttpGet(apiURL);
            JObject JobResponse = JObject.Parse(response);
            bool success = (bool)JobResponse["success"];
            if (!success)
            {
                return null;
            }
            string key = JobResponse["data"].ToString();

            apiURL = "http://xinyun.luyuan.cn/api.ashx?cla=BAILI.Modules.User.BLL.LoginAction&fn=getUserByPhone&key=" + key;
            JObject requestBody = new JObject();
            requestBody.Add("PhoneNo", "111111");
            requestBody.Add("Pwd", "MTlmOGZkYmMyNzI3NTM4NGRlNjRlNDUxYzhmM2FkMTA=");
            response = Common.HttpClientHelper.Request(apiURL, requestBody.ToString(Newtonsoft.Json.Formatting.None));

            JobResponse = JObject.Parse(response);
            success = (bool)JobResponse["success"];
            if (!success)
            {
                return null;
            }

            apiURL = "http://xinyun.luyuan.cn/api.ashx?cla=BAILI.Modules.Nebula.Controller.DeviceOrderHandler&fn=sendHex&key=" + key;
            Models.IOT.Command cmd = this.Create(terminalID,uniCode);
            requestBody = new JObject();
            requestBody.Add("mid", terminalID);
            requestBody.Add("cmd", cmd.HexPacket);
            response = Common.HttpClientHelper.HttpPost(apiURL, requestBody.ToString(Newtonsoft.Json.Formatting.None));

            return JObject.Parse(response);
        }
    }
}
