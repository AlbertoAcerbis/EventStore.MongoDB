using System;
using FourSolid.EventStore.Shared.Abstracts;
using FourSolid.EventStore.Shared.Configuration;
using MongoDB.Driver;

namespace FourSolid.EventStore.MongoDb.Persistence.Repository
{
    public class MongoConnectionHandler<TEntity> where TEntity : IDocumentEntity
    {
        public IMongoCollection<TEntity> MongoCollection { get; }

        public MongoConnectionHandler(EventStoreConfiguration eventStoreConfiguration)
        {
            try
            {
                var mongoClientSettings = 
                    MongoClientSettings.FromUrl(new MongoUrl(eventStoreConfiguration.MongoDbConnectionString));
                var mongoClient = new MongoClient(mongoClientSettings);

                var mongoDatabase = mongoClient.GetDatabase("4SolidEventStore");
                
                var typeName = typeof(TEntity).Name.ToLower();
                typeName = typeName.Replace("nosql", "");

                this.MongoCollection = mongoDatabase.GetCollection<TEntity>(typeName + "Stream");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
    }
}