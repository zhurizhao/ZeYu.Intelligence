using System;
using System.Collections.Generic;
using System.Text;

namespace ZeYu.Intelligence.Monitor.Model
{
    public class Alarm
    {
        public long ID { get; set; }
        public string EventID { get; set; }
        public int AlarmNO { get; set; }
        public string Parms { get; set; }
        public long TerminalID { get; set; }
        public long SamplingTime { get; set; }
    }
}
