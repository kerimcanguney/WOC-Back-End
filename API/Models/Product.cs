using API.ProductModels;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class Product
    {
        public Product(string Name, ProductEnums.ProductTypeEnum Type, ProductEnums.ProductCategoryEnum Category)
        {
            this.Name = Name;
            this.Type = Type.ToString();
            this.Category = Category.ToString();
        }
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public object Info { get; set; }

    }
}
