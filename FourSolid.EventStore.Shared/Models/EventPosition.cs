namespace FourSolid.EventStore.Shared.Models
{
    public class EventPosition
    {
        public long CommitPosition { get; private set; }

        public EventPosition(long commitPosition)
        {
            this.CommitPosition = commitPosition;
        }

        public void IncrementCommitPosition()
        {
            this.CommitPosition += 1;
        }
    }
}