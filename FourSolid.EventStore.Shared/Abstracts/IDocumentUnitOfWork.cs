using System;
using FourSolid.EventStore.Shared.Documents;

namespace FourSolid.EventStore.Shared.Abstracts
{
    public interface IDocumentUnitOfWork : IDisposable
    {
        IDocumentRepository<NoSqlEventData> NoSqlEventDataRepository { get; }
        IDocumentRepository<NoSqlPosition> NoSqlPositionRepository { get; }
    }
}