using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class InsuranceContract
    {
        public string ID { get; set; }
        public string InsuranceCompanyID { get; set; }
        public string InsuranceCompany { get; set; }
        public string UserID { get; set; }
        public string OrderID { get; set; }
        public string Applicant { get; set; }
        public string InsuredObject { get; set; }
        public string Beneficiary { get; set; }
        public long CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public decimal Premium { get; set; }
        public decimal ClaimAmount { get; set; }
    }
}
