namespace FourSolid.EventStore.Shared.Enums
{
    public struct ResolvedEvent
    {
        public readonly RecordedEvent Event;
        public readonly RecordedEvent Link;
        //public readonly Position? OriginalPosition;

        public RecordedEvent OriginalEvent { get; }
        public bool IsResolved { get; }
        public string OriginalStreamId { get; }
        public long OriginalEventNumber { get; }
    }
}