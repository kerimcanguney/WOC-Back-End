using API.Models;
using API.ProductModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProductSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            bool existProduct = products.Find(x => true).Any();
            if (!existProduct)
            {
                products.InsertManyAsync(GetPreconfiguredProducts());
            }
        }
        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            List<Info> info1 = new();
            info1.Add(new Info() { Name = "Calorieen", Value = "20kcal" });
            info1.Add(new Info() { Name = "Gewicht", Value = "200g" });
            List<Info> info2 = new();
            info2.Add(new Info() { Name = "Calorieen", Value = "10kcal" });
            info2.Add(new Info() { Name = "Gewicht", Value = "100g" });
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Appel",
                    Type = "Fruit",
                    Category = "Food",
                    Info = info1
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Banaan",
                    Type = "Fruit",
                    Category = "Food",
                    Info = info2
                },
            };
        }
    }
}
