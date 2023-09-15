using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductApi.Data.Entity
{
	public class TransactionEntity
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? Name { get; set; }
        public Double Amount { get; set; }
        public Int32 Total { get; set; }
    }
}
