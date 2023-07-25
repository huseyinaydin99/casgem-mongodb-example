using CasgemMongoDbCase.Models;
using MongoDB.Driver;

namespace CasgemMongoDbCase.DAL
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Book> Books;
        public IMongoCollection<Writer> Writers;
        public IMongoCollection<City> Cities;
        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("LibraryDb");

            Books = _database.GetCollection<Book>("Books");
            Writers = _database.GetCollection<Writer>("Writers");
            Cities = _database.GetCollection<City>("Cities");
        }
    }
}