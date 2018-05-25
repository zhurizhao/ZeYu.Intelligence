using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class Vehicle
    {
        public string ID { get; set; }
        public string VIN { get; set; }
        public string PlateNumber { get; set; }
        public string Brand { get; set; }
        public string UserID { get; set; }
        public string ModelNumber { get; set; }
        public string TerminalID { get; set; }
        public string PolicyNo { get; set; }
        public int Insured { get; set; }
        public string Remark { get; set; }
        public DateTime RegisterTime { get; set; }
        public long CreateTime { get; set; }
    }
}
