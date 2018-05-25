using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Business
{
    public abstract class AbstractCommand
    {

        public abstract JObject Send(long terminalID, string uniCode ,string apiURL, string accessSecret=null);


        /// <summary>
        /// 根据数据配置文件创建指令实体
        /// </summary>
        /// <param name="terminalID"></param>
        /// <param name="uniCode"></param>
        /// <returns></returns>
        public  Models.IOT.Command Create(long terminalID,string uniCode) //,string transeURL,string accessSecret
        {

            string hexTerminalID = Convert.ToString(terminalID, 16).PadLeft(8, '0');
            long nowTimestamp = Common.Time.GetUTCTimestamp(DateTime.Now);
            string hexTimeStamp8 = nowTimestamp.ToString("x2").PadLeft(16, '0'); //Convert.ToString(nowTimestamp, 16)
            string hexTimeStamp6 = (nowTimestamp / 1000).ToString("x2").PadLeft(12, '0');
            string hexTimeStamp4 = (nowTimestamp / 1000).ToString("x2").PadLeft(8, '0');


            Models.IOT.Instruction ins = Data.Instruction.Get(uniCode);


            if (!string.IsNullOrWhiteSpace(ins.ChildCmd))
            {
                
                ins.ChildCmd = ins.ChildCmd.Replace("{Timestamp4}", hexTimeStamp4);
                byte[] childCmd = Common.Hex.Hex2Bytes(ins.ChildCmd);
                int childLen = childCmd.Length;
                childCmd[childLen - 2] = COCOPASS.Helper.NetCoreHelper.Encrypt.Xor(childCmd);
                ins.Cmd = ins.Cmd.Replace("{ChildCmd}", Common.Hex.Bytes2Hex(childCmd).Replace("-", ""));
            }

            ins.Cmd = ins.Cmd.Replace("{TerminalID}", hexTerminalID);
            ins.Cmd = ins.Cmd.Replace("{Timestamp6}", hexTimeStamp6);

            byte[] cmd = Common.Hex.Hex2Bytes(ins.Cmd);
            int len = cmd.Length;
            int[] intArrayCmd = new int[len];
            cmd[len - 2] = COCOPASS.Helper.NetCoreHelper.Encrypt.Xor(cmd);
            ins.Cmd = Common.Hex.Bytes2Hex(cmd).Replace("-","");

            for (int i=0; i<len; i++)
            {
                intArrayCmd[i] = (sbyte)cmd[i];
            }
  

            Models.IOT.Command command = new Models.IOT.Command();
            command.Packet = intArrayCmd;
            //command.BytePacket = cmd;
            command.HexPacket = ins.Cmd;
            command.TerminalID = terminalID;
            //command.Sign = COCOPASS.Helper.NetCoreHelper.Encrypt.MD5(accessSecret + terminalID + command.HexPacket + accessSecret);

            return command;
        }
    }
}
