using ITH.ArchProg.T3.Banking.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Events
{
    public interface IEvent : IRequest
    {
        DateTime Timestamp { get; set; }

        Entity Entity { get; set; }
    }
}
