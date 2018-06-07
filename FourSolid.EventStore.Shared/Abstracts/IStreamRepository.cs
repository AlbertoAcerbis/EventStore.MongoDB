using System;
using System.Threading.Tasks;
using FourSolid.CommonDomain;

namespace FourSolid.EventStore.Shared.Abstracts
{
    public interface IStreamRepository
    {
        Task<IDocumentEntity> GetByIdAsync(Guid id);
        Task InsertOneAsync(IAggregate aggregate);
    }
}