using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class LocationLog
    {
        public long ID { get; set; }
        public string TerminalID { get; set; }
        public string StationID { get; set; }
        public string BDLocation { get; set; }
        public string GDLocation { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public long SamplingTime { get; set; }
        
    }
}
