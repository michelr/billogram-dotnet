using Newtonsoft.Json;

namespace Billogram.Integration
{
    public class Address
    {
        [JsonProperty("street_address")]
        public string Street { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}