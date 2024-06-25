using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Multitenant.Domain.Entities;

public class Tenant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public Guid UniqueId { get; set; }
    public string Secret { get; set; }
}