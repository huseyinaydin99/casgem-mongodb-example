using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CasgemMongoDbCase.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("BookName")]
        public string BookName { get; set; }

        [BsonElement("WriterId")]
        public ObjectId WriterId { get; set; }

        [BsonElement("PageNumber")]
        public int PageNumber { get; set; }

        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonElement("Point")]
        public int Point { get; set; }
    }
}
