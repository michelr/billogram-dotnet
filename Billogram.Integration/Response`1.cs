using Newtonsoft.Json;

namespace Billogram.Integration
{
    public class Response<T>
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
