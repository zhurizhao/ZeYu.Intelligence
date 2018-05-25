using System;
using System.Collections.Generic;
using System.Text;

namespace ZeYu.Intelligence.Monitor.Model
{
    public class Ride
    {
        public long ID { get; set; }
        public long TerminalID { get; set; }
        public string EventID { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int Duration { get; set; }
    }
}
