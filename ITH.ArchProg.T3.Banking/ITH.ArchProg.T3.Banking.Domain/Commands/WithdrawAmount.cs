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
    public class WithdrawAmount : ICommand
    {
        [JsonProperty("bankAccountId")]
        [Required]
        public string BankAccountId { get; set; }

        [JsonProperty("amount")]
        [Required]
        public decimal Amount { get; set; }

        [JsonProperty("reciever")]
        [Required]
        public string Reciever { get; set; }
    }
}
