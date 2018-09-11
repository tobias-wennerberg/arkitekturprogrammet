using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.ReadModels
{
    [JsonObject]
    public class BankTransaction
    {
        [JsonProperty]
        [Required]
        public string Id { get; set; }

        [JsonProperty]
        [Required]
        public string BankAccountId { get; set; }

        [JsonProperty]
        [Required]
        public decimal Amount { get; set; }

        [JsonProperty]
        [Required]
        public string Sender { get; set; }

        [JsonProperty]
        [Required]
        public string Reciever { get; set; }

        [JsonProperty]
        [Required]
        public DateTime Timestamp { get; set; }
    }
}
