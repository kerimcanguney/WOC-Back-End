using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using MongoDB.Driver;
namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;
        public ProductService()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://host.docker.internal:27017");
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
            };
            MongoClient dbClient = new MongoClient(settings);
            //MongoClient dbClient = new MongoClient("mongodb://host.docker.internal:27017");
            var db = dbClient.GetDatabase("products");
            _products = db.GetCollection<Product>("products");
            ProductSeed.SeedData(_products);
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
        public Product? GetProduct (string id)
        {
            var prod = _products.Find(x => x.Id == id).FirstOrDefault();
            return prod;
        }

        public void updateProduct (string id, Product product)
        {
            var prod = _products.ReplaceOne(x => x.Id == id, product);
            return ;
        }
        public void deleteProduct(string id)
        {
            var prod = _products.DeleteOne(x => x.Id == id);
            return;
        }
    }
}
