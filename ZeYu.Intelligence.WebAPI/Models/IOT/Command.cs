using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models.IOT
{
    public class Command
    {
        public long TerminalID { get; set; }
        
        //public byte[] BytePacket { get; set; } //较好的做法是JSON工具类会自动将byte[]转换成base64编码字符串。而JAVAjson会转换成一个数字数组 如 [0,-123,98,....]
        public string HexPacket { get; set; }
        public int[] Packet { get; set; } //兼容java Gson的序列化转换
        //public string Sign { get; set; }
    }
}
