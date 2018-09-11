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
    internal static class RecordedEventMapper
    {
        internal static IEvent ToDomainEvent(this RecordedEvent recordedEvent)
        {
            return recordedEvent.ToDomainEvent<IEvent>();
        }

        internal static TEvent ToDomainEvent<TEvent>(this RecordedEvent recordedEvent)
        {
            var value = Encoding.UTF8.GetString(recordedEvent.Data);
            var eventType = Type.GetType(recordedEvent.EventType);
            return (TEvent)JsonConvert.DeserializeObject(value, eventType);
        }
    }
}
