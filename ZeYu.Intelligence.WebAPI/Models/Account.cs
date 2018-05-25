using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class Account
    {
        public string ID { get; set; }
        public string MobileNO { get; set; }
        public string EMail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string AppID { get; set; }
        public int Status { get; set; }
        public long RegisterTime { get; set; }
    }
}
