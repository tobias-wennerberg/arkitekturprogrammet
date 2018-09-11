using ITH.ArchProg.T3.Banking.Domain.Entities;
using ITH.ArchProg.T3.Banking.Domain.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Domain.Repositories
{
    public interface IEventRepository
    {
        Task Save(IEvent @event);

        void Clear();

        IReadOnlyList<IEvent> GetPrevious(Entity entity);

        Task<TEvent> GetLast<TEntity, TEvent>()
            where TEntity : Entity
            where TEvent : IEvent;
    }
}
