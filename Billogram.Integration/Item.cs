using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billogram.Integration
{
    public class Item
    {
        [JsonProperty("item_no")]
        public string Id { get; set; }

        [JsonProperty("count")]
        public double Count { get; set; }

        [JsonProperty("discount", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double Discount { get; set; }
    }
}
