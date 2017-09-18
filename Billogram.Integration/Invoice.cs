using Newtonsoft.Json;
using System;

namespace Billogram.Integration
{
    public class Invoice
    {
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("reminder_fee")]
        public double ReminderFee { get; set; }

        [JsonProperty("interest_rate")]
        public double InterestRate { get; set; }

        [JsonProperty("automatic_reminders")]
        public bool AutoReminders { get; set; }

        [JsonProperty("automatic_reminders_settings")]
        public Reminder[] AutoRemindersSettings { get; set; }

        [JsonProperty("due_date")]
        public DateTime DueDate { get; set; }

        [JsonProperty("info", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Info Information { get; set; }
    }
}
