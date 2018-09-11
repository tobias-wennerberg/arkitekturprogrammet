using ITH.ArchProg.T3.Banking.Domain.Entities;
using ITH.ArchProg.T3.Banking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITH.ArchProg.T3.Banking.Infrastructure.EventSourcing
{
    public interface IEventStoreManager
    {
        void StartProjection<TEntity>(Projection<TEntity> projection) where TEntity : Entity;

        void StopProjections();

        void EnableSystemCategoryProjection();

        void EnableCategoryProjection<TEntity>() where TEntity : Entity;
    }
}
