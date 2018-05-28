namespace FourSolid.EventStore.MongoDb.Models
{
    public static class ExpectedVersion
    {
        public const long Any = -2;
        public const long NoStream = -1;
        public const long EmptyStream = -1;
        public const long StreamExists = -4;
    }
}