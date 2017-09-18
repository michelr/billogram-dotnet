using Newtonsoft.Json;
using System;

namespace Billogram.Integration
{
    public class Info
    {
        [JsonProperty("order_no")]
        public int OrderNo { get; set; }

        [JsonProperty("order_date")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}