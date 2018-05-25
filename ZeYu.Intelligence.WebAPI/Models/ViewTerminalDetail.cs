using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class ViewTerminalDetail
    {
        public long ID { get; set; }
        public string SN { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string IMEI { get; set; }
        public string MAC { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }       
        public long ValidDate { get; set; }
        public string ReceiveHost { get; set; }
        public int ReceiveTCPPort { get; set; }
        public int ReceiveUDPPort { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public long AddDate { get; set; }
        public string ProtocolVersion { get; set; }
        public string FirmwareVersion { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        //public float Longitude { get; set; }
        //public float latitude { get; set; }
        public string BDLocation { get; set; } 
        public string Address { get; set; }
        public string ModelNumber { get; set; }
    }
}
