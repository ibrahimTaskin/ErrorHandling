using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class Products
    {
        [BsonId]
        public ObjectId _Id { get; set; }
        public string Name { get; set; }
    }
}