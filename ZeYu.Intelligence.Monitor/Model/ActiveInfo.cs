using System;
using System.Collections.Generic;
using System.Text;

namespace ZeYu.Intelligence.Monitor.Model
{
    public class ActiveInfo
    {
        public long Id { get; set; }

        public string DataType { get; set; }

        public long TerminalID { get; set; }

        public long LatestSamplingTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public bool Online { get; set; }
    }
}
