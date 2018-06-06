using System;

namespace FourSolid.EventStore.Shared.Models
{
    public sealed class EventData
    {
        public readonly Guid EventId;
        public readonly string Type;
        public readonly bool IsJson;
        public readonly byte[] Data;
        public readonly byte[] Metadata;

        public EventData(Guid eventId, string type, bool isJson, byte[] data, byte[] metadata)
        {
            this.EventId = eventId;
            this.Type = type;
            this.IsJson = isJson;
            this.Data = data;
            this.Metadata = metadata;
        }
    }
}