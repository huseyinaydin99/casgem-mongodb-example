using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CasgemMongoDbCase.Models
{
    public class WriterViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string CityId { get; set; }
    }
}
