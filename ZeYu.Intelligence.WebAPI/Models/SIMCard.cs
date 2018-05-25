using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class SIMCard
    {
        public long ID { get; set; }
        
        public string NO { get; set; }
        public string ICCID { get; set; }
        public string ISMI { get; set; }
        public string PUK { get; set; }
        public string PIN { get; set; }
        public decimal Balance { get; set; }
        public string SupplierID { get; set; }
        public int Status { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
