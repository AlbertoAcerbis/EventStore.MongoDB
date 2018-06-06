using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourSolid.CommonDomain;
using MongoDB.Driver;

namespace FourSolid.EventStore.Shared.Abstracts
{
    public interface IDocumentRepository<TEntity> where TEntity : IDocumentEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task InsertOneAsync(TEntity documentToInsert);

        Task ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity documentToUpdate);

        Task DeleteOneAsync(FilterDefinition<TEntity> filter);

        Task<IList<TEntity>> FindAsync(FilterDefinition<TEntity> filter);

        Task UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
    }
}