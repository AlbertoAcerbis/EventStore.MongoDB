using System.Collections.Generic;
using System.Linq;
using FourSolid.CommonDomain;
using FourSolid.EventStore.Shared.Abstracts;
using FourSolid.EventStore.Shared.Models;

namespace FourSolid.EventStore.Shared.Documents
{
    public class NoSqlEventData : DocumentEntity
    {
        public IEnumerable<NoSqlEvent> EventStream { get; private set; }

        protected NoSqlEventData()
        {
        }

        #region ctor
        public static NoSqlEventData CreateNoSqlEventData(IAggregate aggregate)
        {
            return new NoSqlEventData(aggregate);
        }

        private NoSqlEventData(IAggregate aggregate)
        {
            this.Id = aggregate.Id.ToString();
            this.EventStream = Enumerable.Empty<NoSqlEvent>();
        }
        #endregion

        public void AppendEvent(EventData eventData)
        {
            NoSqlEvent[] eventToAdd =
            {
                NoSqlEvent.CreateNoSqlEvent(eventData.EventId.ToString(), eventData.Type, eventData.IsJson, eventData.Data, eventData.Metadata)
            };
            this.EventStream = this.EventStream.Concat(eventToAdd);
        }
    }
}