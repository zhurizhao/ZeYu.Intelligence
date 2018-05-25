using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class UserInfo
    {
        public string ID { get; set; }
        public string AccountID { get; set; }
        public int Sex { get; set; }
        public string RealName { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string MobileNumber { get; set; }
        public string TelPhoneNumber { get; set; }
        public string Remark { get; set; }
        public long CreateTime { get; set; }
    }
}
