using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CasgemMongoDbCase.Models
{
    public class CityViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
