namespace FourSolid.EventStore.Shared.Documents
{
    public class NoSqlEvent
    {
        public string EventId { get; private set; }
        public string Type { get; private set; }
        public bool IsJson { get; private set; }
        public byte[] Data { get; private set; }
        public byte[] Metadata { get; private set; }

        protected NoSqlEvent()
        { }

        public static NoSqlEvent CreateNoSqlEvent(string eventId, string type, bool isJson, byte[] data,
            byte[] metadata)
        {
            return new NoSqlEvent(eventId, type, isJson, data, metadata);
        }

        private NoSqlEvent(string eventId, string type, bool isJson, byte[] data, byte[] metadata)
        {
            this.EventId = eventId;
            this.Type = type;
            this.IsJson = isJson;
            this.Data = data;
            this.Metadata = metadata;
        }
    }
}