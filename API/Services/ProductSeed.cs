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
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Appel",
                    Type = "Fruit",
                    Category = "Food"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Speakers",
                    Type = "Fruit",
                    Category = "Food"

                },
            };
        }
    }
}
