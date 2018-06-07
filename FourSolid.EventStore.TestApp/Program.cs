using System;
using FourSolid.EventStore.MongoDb;
using FourSolid.EventStore.MongoDb.Persistence.Factory;
using FourSolid.EventStore.Shared.Configuration;
using FourSolid.EventStore.TestApp.Entities;

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

            var deviceGuid = Guid.NewGuid();
            var device = Device.CreateDevice(deviceGuid, "AB1234DE", "AB1234DE", DateTime.UtcNow);
            eventStoreRepository.SaveAsync(device, Guid.NewGuid(), d => { }).GetAwaiter().GetResult();

            device = eventStoreRepository.GetByIdAsync<Device>(deviceGuid).GetAwaiter().GetResult();
            device.UpdateSerialNumber("BC2345EF");
            eventStoreRepository.SaveAsync(device, Guid.NewGuid(), d => { }).GetAwaiter().GetResult();

            Console.WriteLine($"Device Id: {device.Id}");
            Console.WriteLine($"Serial Number: {device.GetSerialNumber()}");
            Console.ReadLine();
        }
    }
}
