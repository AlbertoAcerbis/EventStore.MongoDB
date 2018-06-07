using FourSolid.Common.InProcessBus.Abstracts;
using FourSolid.EventStore.Shared.Abstracts;
using FourSolid.EventStore.Shared.Configuration;

namespace FourSolid.EventStore.MongoDb
{
    public class EventsDispatcher
    {
        private readonly string _eventClrTypeHeader;        // "FourSolidEventName";
        private readonly string _aggregateClrTypeHeader;    // "FourSolidAggregateName";

        private const int WritePageSize = 500;
        private const int ReadPageSize = 500;
        private const string CommitIdHeader = "CommitId";

        private readonly IEventDataFactory _eventDataFactory;

        public EventsDispatcher(EventStoreConfiguration eventStoreConfiguration, IEventDataFactory eventDataFactory, IEventBus eventBus)
        {
            this._eventClrTypeHeader = eventStoreConfiguration.EventTypeHeader;
            this._aggregateClrTypeHeader = eventStoreConfiguration.AggregateTypeHeader;

            this._eventDataFactory = eventDataFactory;
        }
    }
}