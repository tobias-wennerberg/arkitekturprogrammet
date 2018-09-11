using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Commands
{
    [JsonObject]
    public class DepositAmount : ICommand
    {
        [JsonProperty("bankAccountId")]
        [Required]
        public string BankAccountId { get; set; }

        [JsonProperty("amount")]
        [Required]
        public decimal Amount { get; set; }

        [JsonProperty("sender")]
        [Required]
        public string Sender { get; set; }

    }
}
