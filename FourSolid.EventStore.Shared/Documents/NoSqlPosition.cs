using FourSolid.EventStore.Shared.Abstracts;
using MongoDB.Driver;

namespace FourSolid.EventStore.Shared.Documents
{
    public class NoSqlPosition : DocumentEntity
    {
        public long CommitPosition { get; private set; }

        protected NoSqlPosition()
        { }

        #region ctor
        public static NoSqlPosition CreateNoSqlPosition(long commitPosition)
        {
            return new NoSqlPosition(commitPosition);
        }

        private NoSqlPosition(long commitPosition)
        {
            this.CommitPosition = commitPosition;
        }
        #endregion

        public UpdateDefinition<NoSqlPosition> UpdatePosition(long commitPosition)
        {
            return Builders<NoSqlPosition>.Update.Set(p => p.CommitPosition, commitPosition);
        }
    }
}