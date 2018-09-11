using EventStore.ClientAPI;
using ITH.ArchProg.T3.Banking.Domain.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Infrastructure.Mappers
{
    internal static class IEventMapper
    {
        internal static EventData ToEventData(this IEvent @event)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { metadata = "metadata" }));
            return new EventData(Guid.NewGuid(), @event.GetType().AssemblyQualifiedName, true, data, metadata);
        }
    }
}
