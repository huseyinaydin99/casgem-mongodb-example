using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CasgemMongoDbCase.Models
{
    public class BookViewModel
    {
        public string Id { get; set; }
        public string BookName { get; set; }

        public string WriterId { get; set; }

        public int PageNumber { get; set; }
        public string Type { get; set; }
        public int Point { get; set; }
    }
}
