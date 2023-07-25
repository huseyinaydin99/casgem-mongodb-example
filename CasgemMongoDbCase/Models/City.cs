using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CasgemMongoDbCase.Models
{
    public class City
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
