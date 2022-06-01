using API.ProductModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categories;
        public CategoryService()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://host.docker.internal:27017");
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
            };
            MongoClient dbClient = new MongoClient(settings);
            //MongoClient dbClient = new MongoClient("mongodb://host.docker.internal:27017");
            var db = dbClient.GetDatabase("categories");
            _categories = db.GetCollection<Category>("categories");
            CategorySeed.SeedData(_categories);
        }

        public Category AddCategory(Category category)
        {
            _categories.InsertOne(category);
            return category;
        }

        public void deleteCategory(string id)
        {
            var cat = _categories.DeleteOne(x => x.Id == id);
            return;
        }

        public List<Category> GetCategories()
        {
            return _categories.Find(x => true).ToList();
        }

        public Category? GetCategory(string id)
        {
            var cat = _categories.Find(x => x.Id == id).FirstOrDefault();
            return cat;
        }

        public Category? updateCategory(string id, Category category)
        {
            var cat = _categories.ReplaceOne(x => x.Id == id, category);
            return category;
        }
    }
}
