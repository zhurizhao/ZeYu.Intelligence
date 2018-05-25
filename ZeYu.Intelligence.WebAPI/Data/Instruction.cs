using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Data
{
    public class Instruction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Models.IOT.Instruction Get(string unicode)
        {
            string key = "INS:" + unicode;
            string json = new Common.Redis.Helper().StringGet(key);
            if(!string.IsNullOrEmpty(json))
            {
               
                Models.IOT.Instruction ins = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.IOT.Instruction>(json);
                ins.Cmd = ins.Cmd.Replace("-", "").Replace(" ", "");
                if(!string.IsNullOrEmpty(ins.ChildCmd))
                    ins.ChildCmd = ins.ChildCmd.Replace("-", "").Replace(" ", "");
                return ins ;
            }
            
            return null;
        }
    }
}
