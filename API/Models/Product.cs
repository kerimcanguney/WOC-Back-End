using API.ProductModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

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
        public Product()
        {

        }
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public IList<Info> Info { get; set; }

    }
    public class Info
    {
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
