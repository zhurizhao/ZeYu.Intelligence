using System;
using System.Collections.Generic;
using System.Text;

namespace ZeYu.Intelligence.Monitor.Model
{
    public class Charge
    {
        public long ID { get; set; }
        public long TerminalID { get; set; }
        public string EventID { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int Duration { get; set; }
        public double StartVoltage { get; set; }
        public double EndVoltage { get; set; }
    }
}
