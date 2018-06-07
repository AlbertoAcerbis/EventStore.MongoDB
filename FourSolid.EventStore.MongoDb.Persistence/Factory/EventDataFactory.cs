using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourSolid.CommonDomain;
using FourSolid.EventStore.MongoDb.Persistence.Repository;
using FourSolid.EventStore.Shared.Abstracts;
using FourSolid.EventStore.Shared.Configuration;
using FourSolid.EventStore.Shared.Documents;
using FourSolid.EventStore.Shared.Models;
using MongoDB.Driver;

namespace FourSolid.EventStore.MongoDb.Persistence.Factory
{
    public class EventDataFactory : IEventDataFactory
    {
        private readonly IDocumentUnitOfWork _documentUnitOfWork;

        public EventDataFactory(EventStoreConfiguration eventStoreConfiguration)
        {
            this._documentUnitOfWork = new DocumentUnitOfWork(eventStoreConfiguration);
        }

        public async Task AppendToStreamAsync(IAggregate aggregate, long expectedVersion, IEnumerable<EventData> events)
        {
            try
            {
                var filter = Builders<NoSqlEventData>.Filter.Eq("_id", aggregate.Id.ToString());
                var documentsResult = await this._documentUnitOfWork.NoSqlEventDataRepository.FindAsync(filter);

                if (!documentsResult.Any())
                {
                    var noSqlDocument = NoSqlEventData.CreateNoSqlEventData(aggregate);
                    await this._documentUnitOfWork.NoSqlEventDataRepository.InsertOneAsync(noSqlDocument);

                    for (var i = 0; i <= 5; i++)
                    {
                        documentsResult = await this._documentUnitOfWork.NoSqlEventDataRepository.FindAsync(filter);
                        if (!documentsResult.Any())
                            break;

                        Thread.Sleep(100);
                    }
                }

                if (!documentsResult.Any())
                    return;

                var noSqlAggregate = documentsResult.First();
                var eventDatas = events as EventData[] ?? events.ToArray();
                foreach (var eventData in eventDatas)
                {
                    noSqlAggregate.AppendEvent(eventData);
                }

                await this._documentUnitOfWork.NoSqlEventDataRepository.ReplaceOneAsync(filter, noSqlAggregate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<EventData>> ReadStreamEventsForwardAsync(Guid aggregateId, long startPosition, int eventsCount)
        {
            try
            {
                var filter = Builders<NoSqlEventData>.Filter.Eq("_id", aggregateId.ToString());
                var documentsResult = await this._documentUnitOfWork.NoSqlEventDataRepository.FindAsync(filter);

                if (!documentsResult.Any())
                    return Enumerable.Empty<EventData>();

                var noSqlEventData = documentsResult.First();

                return noSqlEventData.EventStream.Any()
                    ? noSqlEventData.EventStream.Select(noSqlEvent => noSqlEvent.ToEventData())
                    : Enumerable.Empty<EventData>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        #region Dispose
        private bool _disposed; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._documentUnitOfWork.Dispose();
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