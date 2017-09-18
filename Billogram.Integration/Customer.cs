using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billogram.Integration
{
    public class Customer
    {
        [JsonProperty(
            "customer_no", 
            Required = Required.AllowNull, 
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int CustomerNo { get; set; }

        [JsonProperty("name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("org_no", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CivicRegNo { get; set; }

        [JsonProperty("company_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("contact", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ContactInfo ContactInfo { get; set; }

        [JsonProperty("address", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Address Address { get; set; }

        [JsonProperty("created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime Created { get; set; }
    }
}
