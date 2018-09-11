using ITH.ArchProg.T3.Banking.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ITH.ArchProg.T3.Banking.Domain.Events
{
    [JsonObject]
    public class BankAccountCreated : IEvent
    {
        [JsonProperty("id")]
        [Required]
        public string Id { get; set; }

        [JsonProperty("clearingNo")]
        [Required]
        public int ClearingNo { get; set; }

        [JsonProperty("accountNo")]
        [Required]
        public string AccountNo { get; set; }

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

        [JsonProperty("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public Entity Entity { get; set; }
    }
}