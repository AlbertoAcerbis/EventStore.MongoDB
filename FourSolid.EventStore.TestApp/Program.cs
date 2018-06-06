using System;
using FourSolid.EventStore.MongoDb;
using FourSolid.EventStore.MongoDb.Persistence.Factory;
using FourSolid.EventStore.Shared.Configuration;

namespace FourSolid.EventStore.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStoreConfiguration = new EventStoreConfiguration
            {
                MongoDbConnectionString = "mongodb://4Solid:4$olid!2016@cluster0-shard-00-00-5pgij.mongodb.net:27017,cluster0-shard-00-01-5pgij.mongodb.net:27017,cluster0-shard-00-02-5pgij.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin",
                EventTypeHeader = "FourSolidEventName",
                AggregateTypeHeader = "FourSolidAggregateName"
            };

            var eventDataFactory = new EventDataFactory(eventStoreConfiguration);

            var eventStoreRepository = new EventStoreRepository(eventStoreConfiguration, eventDataFactory);

            //eventStoreRepository.SaveAsync()
        }
    }
}
