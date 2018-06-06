using FourSolid.EventStore.Shared.Enums;

namespace FourSolid.EventStore.Shared.Models
{
    public class StreamEventsSlice
    {
        public readonly SliceReadStatus Status;
        public readonly string Stream;
        public readonly long FromEventNumber;
        public readonly ReadDirection ReadDirection;
        public readonly ResolvedEvent[] Events;
        public readonly long NextEventNumber;
        public readonly long LastEventNumber;
        public readonly bool IsEndOfStream;
    }
}