using Newtonsoft.Json;

namespace Billogram.Integration
{
    public class ContactInfo
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}