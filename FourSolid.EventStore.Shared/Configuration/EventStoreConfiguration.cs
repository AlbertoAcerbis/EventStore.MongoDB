namespace FourSolid.EventStore.Shared.Configuration
{
    public class EventStoreConfiguration
    {
        public string MongoDbConnectionString { get; set; }
        public string EventTypeHeader { get; set; }
        public string AggregateTypeHeader { get; set; }
    }
}