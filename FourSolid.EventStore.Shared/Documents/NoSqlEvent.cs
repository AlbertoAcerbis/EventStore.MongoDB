using System;
using FourSolid.EventStore.Shared.Models;

namespace FourSolid.EventStore.Shared.Documents
{
    public class NoSqlEvent
    {
        public string EventId { get; private set; }
        public string Type { get; private set; }
        public bool IsJson { get; private set; }
        public byte[] Data { get; private set; }
        public byte[] Metadata { get; private set; }
        public long CommitPosition { get; private set; }

        protected NoSqlEvent()
        { }

        public static NoSqlEvent CreateNoSqlEvent(string eventId, string type, bool isJson, byte[] data,
            byte[] metadata, long commitPosition)
        {
            return new NoSqlEvent(eventId, type, isJson, data, metadata, commitPosition);
        }

        private NoSqlEvent(string eventId, string type, bool isJson, byte[] data, byte[] metadata, long commitPosition)
        {
            this.EventId = eventId;
            this.Type = type;
            this.IsJson = isJson;
            this.Data = data;
            this.Metadata = metadata;
            this.CommitPosition = commitPosition;
        }

        public EventData ToEventData()
        {
            Guid.TryParse(this.EventId, out var eventGuid);
            return new EventData(eventGuid, this.Type, this.IsJson, this.Data, this.Metadata);
        }
    }
}