using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITH.ArchProg.T3.Banking.Domain.ReadModels
{
    [JsonObject]
    public class BankAccount
    {
        [JsonProperty]
        [Required]
        public string Id { get; set; }

        [JsonProperty]
        [Required]
        public string ClearingNo{ get; set; }

        [JsonProperty]
        [Required]
        public string AccountNo { get; set; }

        [JsonProperty]
        [Required]
        public string CustomerId { get; set; }

        [JsonProperty]
        [Required]
        public string Fullname { get; set; }

        [JsonProperty]
        [Required]
        public string Address { get; set; }

        [JsonProperty]
        [Required]
        public string Phonenumber { get; set; }

        [JsonProperty]
        [Required]
        public decimal Amount { get; set; }

        [JsonProperty]
        [Required]
        public DateTime Timestamp { get; set; }

        [JsonProperty]
        [Required]
        public bool IsClosed { get; set; }
    }
}