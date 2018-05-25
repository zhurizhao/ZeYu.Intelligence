using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class APIResponse
    {
        public int Error { get; set; }
        public string Message { get; set; }
        public object  Body { get; set; }
    }
}
