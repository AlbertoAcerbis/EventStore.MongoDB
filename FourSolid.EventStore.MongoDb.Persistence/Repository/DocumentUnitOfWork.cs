using System;
using FourSolid.EventStore.Shared.Abstracts;
using FourSolid.EventStore.Shared.Configuration;
using FourSolid.EventStore.Shared.Documents;

namespace FourSolid.EventStore.MongoDb.Persistence.Repository
{
    public class DocumentUnitOfWork : IDocumentUnitOfWork
    {
        private readonly EventStoreConfiguration _eventStoreConfiguration;

        #region ctor
        public DocumentUnitOfWork(EventStoreConfiguration eventStoreConfiguration)
        {
            this._eventStoreConfiguration = eventStoreConfiguration;
        }
        #endregion

        private IDocumentRepository<NoSqlEventData> _noSqlEventDataRepository;
        public IDocumentRepository<NoSqlEventData> NoSqlEventDataRepository =>
            this._noSqlEventDataRepository ??
            (this._noSqlEventDataRepository = new DocumentRepository<NoSqlEventData>(this._eventStoreConfiguration));

        private IDocumentRepository<NoSqlPosition> _noSqlPositionRepository;
        public IDocumentRepository<NoSqlPosition> NoSqlPositionRepository =>
            this._noSqlPositionRepository ?? (this._noSqlPositionRepository =
                new DocumentRepository<NoSqlPosition>(this._eventStoreConfiguration));

        #region Dispose
        private bool _disposed; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                this._disposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //~EventStoreRepository()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    this.Dispose(false);
        //}

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}