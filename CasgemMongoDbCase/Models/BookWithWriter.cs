namespace CasgemMongoDbCase.Models
{
    public class BookWithWriter : Book
    {
        public List<Writer> Writers { get; set; }
    }
}
