using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourSolid.CommonDomain;
using FourSolid.EventStore.Shared.Abstracts;
using FourSolid.EventStore.Shared.Configuration;
using MongoDB.Driver;

namespace FourSolid.EventStore.MongoDb.Persistence.Repository
{
    public class DocumentRepository<TEntity> : IDocumentRepository<TEntity> where TEntity : IDocumentEntity
    {
        protected readonly MongoConnectionHandler<TEntity> MongoConnectionHandler;

        public DocumentRepository(EventStoreConfiguration eventStoreConfiguration)
        {
            this.MongoConnectionHandler = new MongoConnectionHandler<TEntity>(eventStoreConfiguration);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq("_id", id.ToString("N"));
                var results = await this.FindAsync(filter);

                return results.First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task InsertOneAsync(TEntity documentToInsert)
        {
            await this.MongoConnectionHandler.MongoCollection.InsertOneAsync(documentToInsert);
        }

        public async Task ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity documentToUpdate)
        {
            await this.MongoConnectionHandler.MongoCollection.ReplaceOneAsync(filter, documentToUpdate);
        }

        public virtual async Task DeleteOneAsync(FilterDefinition<TEntity> filter)
        {
            await this.MongoConnectionHandler.MongoCollection.DeleteOneAsync(filter);
        }

        public virtual async Task<IList<TEntity>> FindAsync(FilterDefinition<TEntity> filter)
        {
            var listaDocumenti = new List<TEntity>();

            using (var cursor = await this.MongoConnectionHandler.MongoCollection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    listaDocumenti.AddRange(batch);
                }
            }

            return listaDocumenti;
        }

        public virtual async Task UpdateOneAsync(FilterDefinition<TEntity> filter,
            UpdateDefinition<TEntity> updateDefinition)
        {
            await this.MongoConnectionHandler.MongoCollection.UpdateOneAsync(filter, updateDefinition);
        }
    }
}