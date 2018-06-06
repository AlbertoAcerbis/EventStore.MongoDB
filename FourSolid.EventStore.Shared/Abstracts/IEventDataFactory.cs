using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourSolid.CommonDomain;
using FourSolid.EventStore.Shared.Models;

namespace FourSolid.EventStore.Shared.Abstracts
{
    public interface IEventDataFactory : IDisposable
    {
        Task AppendToStreamAsync(IAggregate aggregate, long expectedVersion, IEnumerable<EventData> events);

        Task<IEnumerable<EventData>> ReadStreamEventsForwardAsync(Guid aggregateId, long startPosition, int eventsCount);
    }
}