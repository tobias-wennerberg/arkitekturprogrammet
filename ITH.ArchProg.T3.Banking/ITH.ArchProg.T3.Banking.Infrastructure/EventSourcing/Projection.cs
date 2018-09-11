using ITH.ArchProg.T3.Banking.Domain.Entities;
using ITH.ArchProg.T3.Banking.Domain.Events;
using System;

namespace ITH.ArchProg.T3.Banking.Infrastructure.EventSourcing
{
    public abstract class Projection<TEntity> where TEntity : Entity
    {
        public abstract void Project(IEvent @event, long eventNumber);

        public abstract long? GetLastEventNumber();

    }
}
