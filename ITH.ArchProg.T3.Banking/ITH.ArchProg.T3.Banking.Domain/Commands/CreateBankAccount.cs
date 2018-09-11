using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ITH.ArchProg.T3.Banking.Domain.Commands
{
    [JsonObject]
    public class CreateBankAccount : ICommand
    {
        [JsonProperty("clearingNo")]
        [Required]
        public int ClearingNo { get; set; }

        [JsonProperty("customerId")]
        [Required]
        public string CustomerId { get; set; }

        [JsonProperty("fullname")]
        [Required]
        public string Fullname { get; set; }

        [JsonProperty("address")]
        [Required]
        public string Address { get; set; }

        [JsonProperty("phonenumber")]
        [Required]
        public string Phonenumber { get; set; }
    }
}