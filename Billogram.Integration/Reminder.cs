using Newtonsoft.Json;

namespace Billogram.Integration
{
    public class Reminder
    {
        [JsonProperty("delay_days")]
        public double DelayDays { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}