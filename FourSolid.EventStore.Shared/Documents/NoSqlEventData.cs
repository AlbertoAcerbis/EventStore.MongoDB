using System.Collections.Generic;
using System.Linq;
using FourSolid.CommonDomain;
using FourSolid.EventStore.Shared.Abstracts;
using FourSolid.EventStore.Shared.Models;

namespace FourSolid.EventStore.Shared.Documents
{
    public class NoSqlEventData : DocumentEntity
    {
        public string AggregateType { get; private set; }
        public string StreamName { get; private set; }
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
            this.AggregateType = $"{aggregate.GetType().Name}";
            this.StreamName = $"{char.ToLower(aggregate.GetType().Name[0])}{aggregate.GetType().Name.Substring(1)}Events-{aggregate.Id}";
            this.EventStream = Enumerable.Empty<NoSqlEvent>();
        }
        #endregion

        public void AppendEvent(EventData eventData, EventPosition commitPosition)
        {
            NoSqlEvent[] eventToAdd =
            {
                NoSqlEvent.CreateNoSqlEvent(eventData.EventId.ToString(), eventData.Type, eventData.IsJson, eventData.Data, eventData.Metadata, commitPosition.CommitPosition)
            };
            this.EventStream = this.EventStream.Concat(eventToAdd);
        }
    }
}