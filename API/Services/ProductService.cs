using System.Collections.Generic;
using System.Linq;
using API.Models;
using MongoDB.Driver;
namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;
        public ProductService()
        {
            MongoClient dbClient = new MongoClient("mongodb://localhost:27017");
            var db = dbClient.GetDatabase("products");
            _products = db.GetCollection<Product>("products");

        }
        public List<Product> GetProducts()
        {
            return _products.Find(x => true).ToList();
        }
        public Product AddProduct(Product product)
        {
            _products.InsertOne(product);
            return product;
        }
    }
}
