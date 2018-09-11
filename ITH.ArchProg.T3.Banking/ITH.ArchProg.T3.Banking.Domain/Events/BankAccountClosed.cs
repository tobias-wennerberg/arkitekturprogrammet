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
    public class BankAccountClosed : IEvent
    {
        [JsonProperty("id")]
        [Required]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public Entity Entity { get; set; }
    }
}
