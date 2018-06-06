using MongoDB.Bson.Serialization.Attributes;

namespace FourSolid.EventStore.Shared.Abstracts
{
    public abstract class DocumentEntity : IDocumentEntity
    {
        [BsonId]
        public string Id { get; set; }
        public bool IsDeleted { get; protected set; }
    }
}