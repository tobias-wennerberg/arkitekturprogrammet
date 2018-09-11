using ITH.ArchProg.T3.Banking.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Events
{
    [JsonObject]
    public class AmountWithdrawn : IEvent
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

        [JsonProperty("reciever")]
        [Required]
        public string Reciever { get; set; }

        [JsonProperty("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public Entity Entity { get; set; }
    }
}
