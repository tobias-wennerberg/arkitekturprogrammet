﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Commands
{
    [JsonObject]
    public class CloseBankAccount : ICommand
    {
        [JsonProperty("id")]
        [Required]
        public string Id { get; set; }
    }
}
