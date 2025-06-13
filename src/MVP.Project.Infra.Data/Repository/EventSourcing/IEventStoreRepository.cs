using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Project.Domain.Core.Events;

namespace MVP.Project.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}