using FourSolid.EventStore.MongoDb.Persistence.Repository;
using FourSolid.EventStore.Shared.Abstracts;
using FourSolid.EventStore.Shared.Configuration;

namespace FourSolid.EventStore.MongoDb.Mediator
{
    public class DocumentUnitOfWorkFactory
    {
        public static IDocumentUnitOfWork CreateDocumentUnitOfWork(EventStoreConfiguration eventStoreConfiguration)
        {
            return new DocumentUnitOfWork(eventStoreConfiguration);
        }
    }
}