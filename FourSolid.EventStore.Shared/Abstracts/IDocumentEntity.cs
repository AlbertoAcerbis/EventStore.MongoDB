using MongoDB.Bson.Serialization.Attributes;

namespace FourSolid.EventStore.Shared.Abstracts
{
    public interface IDocumentEntity
    {
        [BsonId]
        string Id { get; set; }
    }
}