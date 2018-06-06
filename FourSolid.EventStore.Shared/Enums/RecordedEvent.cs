using System;

namespace FourSolid.EventStore.Shared.Enums
{
    public class RecordedEvent
    {
        public readonly string EventStreamId;
        public readonly Guid EventId;
        public readonly long EventNumber;
        public readonly string EventType;
        public readonly byte[] Data;
        public readonly byte[] Metadata;
        public readonly bool IsJson;
        public DateTime Created;
        public long CreatedEpoch;
    }
}