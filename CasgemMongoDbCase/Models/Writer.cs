using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CasgemMongoDbCase.Models
{
    public class Writer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("CityId")]
        public ObjectId CityId { get; set; }
    }
}
