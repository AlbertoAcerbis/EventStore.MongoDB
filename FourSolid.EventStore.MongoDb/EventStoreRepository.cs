using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourSolid.CommonDomain;
using FourSolid.CommonDomain.Persistence;
using FourSolid.EventStore.MongoDb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FourSolid.EventStore.MongoDb
{
    public class EventStoreRepository : IRepository
    {
        private readonly string _eventClrTypeHeader;        // "FourSolidEventName";
        private readonly string _aggregateClrTypeHeader;    // "FourSolidAggregateName";

        private const string CommitIdHeader = "CommitId";
        private Func<Type, Guid, string> _aggregateIdToStreamName;
        private static readonly JsonSerializerSettings SerializerSettings;

        public async Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : class, IAggregate
        {
            return await GetByIdAsync<TAggregate>(id, int.MaxValue);
        }

        public Task<TAggregate> GetByIdAsync<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate
        {
            if (version <= 0)
                throw new InvalidOperationException("Cannot get version <= 0");

            //var streamName = this._aggregateIdToStreamName(typeof(TAggregate), id);
            //var aggregate = ConstructAggregate<TAggregate>();
        }

        public async Task SaveAsync(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
        {
            var commitHeaders = new Dictionary<string, object>
            {
                { CommitIdHeader, commitId },
                { this._aggregateClrTypeHeader, aggregate.GetType().AssemblyQualifiedName }
            };
            updateHeaders(commitHeaders);

            var streamName = this._aggregateIdToStreamName(aggregate.GetType(), aggregate.Id);
            var newEvents = aggregate.GetUncommittedEvents().Cast<object>().ToList();
            var originalVersion = aggregate.Version - newEvents.Count;
            var expectedVersion = originalVersion == 0 ? ExpectedVersion.NoStream : originalVersion - 1;
            var eventsToSave = newEvents.Select(e => ToEventData(Guid.NewGuid(), e, commitHeaders)).ToList();


            aggregate.ClearUncommittedEvents();
        }

        public async Task SaveAsync(IAggregate aggregate, Guid commitId)
        {
            await this.SaveAsync(aggregate, commitId, d => { });
        }

        #region Helpers
        public static TAggregate ConstructAggregate<TAggregate>()
        {
            return (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);
        }

        private object DeserializeEvent(byte[] metadata, byte[] data)
        {
            try
            {
                var eventClrTypeName = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(this._eventClrTypeHeader).Value;
                return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)eventClrTypeName),
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            }
            catch (Exception ex)
            {
                var dataEncoded = Encoding.UTF8.GetString(data);
                return null;
            }
        }

        private EventData ToEventData(Guid eventId, object evnt, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evnt, SerializerSettings));

            var eventHeaders = new Dictionary<string, object>(headers)
            {
                {
                    this._eventClrTypeHeader, evnt.GetType().AssemblyQualifiedName
                }
            };
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, SerializerSettings));
            var typeName = evnt.GetType().Name;

            return new EventData(eventId, typeName, true, data, metadata);
        }
        #endregion

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