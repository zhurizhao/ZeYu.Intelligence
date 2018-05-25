using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ZeYu.Intelligence.WebAPI.Models.IOT
{
    public class Instruction
    {
        public int ID { get; set; }
        public string UniCode { get; set; }
        public string Name { get; set; }
        public string Encoding { get; set; }
        public string Version { get; set; }
        public string Signalling { get; set; }
        public string Cmd { get; set; }
        public string ChildCmd { get; set; }
        public string Response { get; set; }
        public string ChildResponse { get; set; }
        public string Remark { get; set; }
        public long UpdateTime { get; set; }
    }
}
