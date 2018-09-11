using ITH.ArchProg.T3.Banking.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ITH.ArchProg.T3.Banking.Domain.Events
{
    [JsonObject]
    public class AmountDeposited : IEvent
    {
        [JsonProperty("bankTransactionId")]
        [Required]
        public string BankTransactionId { get; set; }

        [JsonProperty("bankAccountId")]
        [Required]
        public string BankAccountId { get; set; }

        [JsonProperty("amount")]
        [Required]
        public decimal Amount { get; set; }

        [JsonProperty("sender")]
        [Required]
        public string Sender { get; set; }

        [JsonProperty("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public Entity Entity { get; set; }
    }
}
