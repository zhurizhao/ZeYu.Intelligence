using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZeYu.Intelligence.Monitor.Model
{
    public class EbikeOperatingInfo
    {
        public long ID { get; set; }
        public long TerminalID { get; set; }
        public string DataType { get; set; }
        public string EventID { get; set; }
        public string PreEventID { get; set; }
        public double Voltage { get; set; }
        public double Electricity { get; set; }
        public int AlarmNO { get; set; }
        public string AlarmParm { get; set; }
        public string MNC { get; set; }
        public string LAC { get; set; }
        public string Cell { get; set; }
        public int Status { get; set; }
        public int GSMSignalValue { get; set; }
        public long SamplingTime { get; set; }
        public long Time { get; set; }
        public float Version { get; set; }
        public int ModuleID { get; set; }
        public double StaticVoltageA { get; set; }
        public double StaticVoltageB { get; set; }
        public double BatteryTemperature { get; set; }
        public double AmbientTemperature { get; set; }
        public int SignalNumber { get; set; }
        public JArray GSMSignal { get; set; }

    }
}
