using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;
using ITH.ArchProg.T3.Banking.Domain.Entities;
using ITH.ArchProg.T3.Banking.Domain.Events;
using ITH.ArchProg.T3.Banking.Domain.Repositories;
using ITH.ArchProg.T3.Banking.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Infrastructure.EventSourcing
{
    public class EventStoreManager : IEventRepository, IEventStoreManager
    {
        private readonly ConnectionSettingsBuilder settings;
        private readonly IPEndPoint tcpEndpoint;
        private readonly IPEndPoint httpEndpoint;
        private readonly UserCredentials adminUserCredentials;

        internal EventStoreManager()
        {
            //uncomment to enable verbose logging in client.
            settings = ConnectionSettings.Create();//.EnableVerboseLogging().UseConsoleLogger();
            tcpEndpoint = new IPEndPoint(IPAddress.Loopback, 1113);
            httpEndpoint = new IPEndPoint(IPAddress.Loopback, 2113);
            adminUserCredentials = new UserCredentials("admin", "changeit");
        }

        public async Task Save(IEvent @event)
        {
            using (var connection = CreateConnection(tcpEndpoint))
            {
                var stream = GetStreamFrom(@event.Entity);
                var eventData = @event.ToEventData();
                await connection.ConnectAsync();
                await connection.AppendToStreamAsync(stream, ExpectedVersion.Any, eventData);
            }
        }

        public void Clear()
        {
            Directory.Delete(@".\ESData", true);
        }

        public IReadOnlyList<IEvent> GetPrevious(Entity entity)
        {
            var previousEvents = new List<IEvent>();

            using (var connection = CreateConnection(tcpEndpoint))
            {
                connection.ConnectAsync().Wait();

                var stream = GetStreamFrom(entity);

                var streamEventSlice = connection.ReadStreamEventsForwardAsync(stream, StreamPosition.Start, 4000, true).Result;
                previousEvents = streamEventSlice.Events.OrderBy(e => e.Event.EventNumber).Select(e => e.Event.ToDomainEvent()).ToList();
            }
            return previousEvents;
        }

        public async Task<TEvent> GetLast<TEntity, TEvent>() 
            where TEntity : Entity
            where TEvent : IEvent
        {
            TEvent lastEvent = default(TEvent);

            using (var connection = CreateConnection(tcpEndpoint))
            {
                connection.ConnectAsync().Wait();

                var stream = GetStreamFrom<TEntity, TEvent>();

                var eventReadResult = await connection.ReadEventAsync(stream, StreamPosition.End, true);

                if(eventReadResult.Event.HasValue)
                {
                    lastEvent = eventReadResult.Event.Value.Event.ToDomainEvent<TEvent>();
                }
            }
            return lastEvent;
        }

        public void StartProjection<TEntity>(Projection<TEntity> projection) where TEntity : Entity
        {
            //EnableSystemCategoryProjection();

            //EnableCategoryProjection<TEntity>();

            aliveConnection = CreateConnection(tcpEndpoint);

            aliveConnection.ConnectAsync().Wait();

            var stream = GetStreamFrom<TEntity>();

            //How to avoid this? Is it possible to subscribe to not existing stream?
            aliveConnection.AppendToStreamAsync(stream, ExpectedVersion.Any).Wait();

            var checkpoint = projection.GetLastEventNumber() ?? StreamCheckpoint.StreamStart;

            aliveConnection.SubscribeToStreamFrom(stream, checkpoint, CatchUpSubscriptionSettings.Default, (subscription, resolvedEvent) =>
             {
                 IEvent @event = null;

                 try
                 {
                     @event = resolvedEvent.Event.ToDomainEvent();

                 }
                 catch (Exception)
                 {
                     //...
                 }
                 if (@event != null)
                 {
                     projection.Project(@event, resolvedEvent.OriginalEventNumber);
                 }
             });

        }

        public void StopProjections()
        {
            aliveConnection.Dispose();
        }

        private IEventStoreConnection aliveConnection;

        public void EnableSystemCategoryProjection()
        {
            var logger = new ConsoleLogger(); //TODO inject
            var projectionManager = new ProjectionsManager(logger, httpEndpoint, new TimeSpan(0, 1, 0));
            projectionManager.EnableAsync("$by_category", adminUserCredentials).Wait();
        }

        public void EnableCategoryProjection<TEntity>() where TEntity : Entity
        {
            var logger = new ConsoleLogger(); //TODO inject
            var projectionManager = new ProjectionsManager(logger, httpEndpoint, new TimeSpan(0, 1, 0));
            var stream = GetStreamFrom<TEntity>();
            var name = $"by_category_{stream}";

            var status = projectionManager.GetStatusAsync(name, adminUserCredentials).Result;

            if (string.IsNullOrEmpty(status))
            {
                var query = $"fromCategory('{stream}')" +
                                ".when({" +
                                    "'$any' : function(s, e) {" +
                                        "if (e !== null && e.data !== null) {" +
                                            $"linkTo('{stream}', e);" +
                                        "}" +
                                    "}" +
                                "});";
                projectionManager.CreateContinuousAsync(name, query, adminUserCredentials).Wait();
            }
            else
            {
                projectionManager.EnableAsync(name, adminUserCredentials).Wait();
            }
        }

        public void EnableCategoryByEventTypeProjection<TEntity, TEvent>() 
            where TEntity : Entity
            where TEvent : IEvent
        {
            var logger = new ConsoleLogger(); //TODO inject
            var projectionManager = new ProjectionsManager(logger, httpEndpoint, new TimeSpan(0, 1, 0));
            var stream = GetStreamFrom<TEntity, TEvent>();
            var eventTypeName = typeof(TEvent).AssemblyQualifiedName;
            var name = $"by_category_{stream}";

            var status = projectionManager.GetStatusAsync(name, adminUserCredentials).Result;

            if (string.IsNullOrEmpty(status))
            {
                var query = $"fromCategory('{stream}')" +
                                ".when({" +
                                    $"'{eventTypeName}' : function(s, e) {{" +
                                        $"linkTo('{stream}', e);" +
                                    "}" +
                                "});";
                projectionManager.CreateContinuousAsync(name, query, adminUserCredentials).Wait();
            }
            else
            {
                projectionManager.EnableAsync(name, adminUserCredentials).Wait();
            }
        }

        private IEventStoreConnection CreateConnection(IPEndPoint endPoint)
        {
            return EventStoreConnection.Create(settings, endPoint);
        }

        private string GetStreamFrom(Entity entity)
        {
            return $"{entity.GetType().Name}-{entity.Id}";
        }

        private string GetStreamFrom<TEntity>() where TEntity : Entity
        {
            return typeof(TEntity).Name;
        }

        private string GetStreamFrom<TEntity, TEvent>() 
            where TEntity : Entity
            where TEvent : IEvent
        {
            return $"{typeof(TEntity).Name}_{typeof(TEvent).Name}";
        }
    }
}
