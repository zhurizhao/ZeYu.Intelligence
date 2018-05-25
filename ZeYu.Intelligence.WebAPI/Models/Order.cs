using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class Order
    {
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("ShopID")]
        public string ShopID { get; set; }
        [JsonProperty("UnionID")]
        public string UnionID { get; set; }
        [JsonProperty("CreateTime")]
        public long CreateTime { get; set; }
        [JsonProperty("Discount")]
        public decimal Discount { get; set; }
        [JsonProperty("Quantity")]
        public int Quantity { get; set; }
        [JsonProperty("Amount")]
        public decimal Amount { get; set; }
        [JsonProperty("Deposit")]
        public decimal Deposit { get; set; }
        [JsonProperty("PayTypeID")]
        public int PayTypeID { get; set; }
        [JsonProperty("DeliverStatus")]
        public int DeliverStatus { get; set; }
        [JsonProperty("Status")]
        public int Status { get; set; }
        [JsonProperty("Remark")]
        public string Remark { get; set; }
       
        [JsonProperty("ReceiverAddressID")]
        public string ReceiverAddressID { get; set; }
    }
}
