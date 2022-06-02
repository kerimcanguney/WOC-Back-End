using API.ProductModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class CategorySeed
    {
        public static void SeedData(IMongoCollection<Category> categories)
        {
            bool existProduct = categories.Find(x => true).Any();
            if (!existProduct)
            {
                categories.InsertManyAsync(GetPreconfiguredCategories());
            }
        }
        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            //Types 1
            List<Info> info1 = new();
            info1.Add(new Info() { Name = "Calorieen", Value = "" });
            info1.Add(new Info() { Name = "Gewicht", Value = "" });
            info1.Add(new Info() { Name = "Land van herkomst", Value = "" });
            List<Info> info2 = new();
            info2.Add(new Info() { Name = "Calorieen", Value = "" });
            info2.Add(new Info() { Name = "Gewicht", Value = "" });
            info2.Add(new Info() { Name = "Houdbaarheid", Value = "" });
            List<Info> info3 = new();
            info3.Add(new Info() { Name = "Producent", Value = "" });
            info3.Add(new Info() { Name = "Houdbaarheid", Value = "" });
            List<ProductModels.Type> types1 = new();
            types1.Add(new ProductModels.Type() { Name = "Fruit", Info = info1});
            types1.Add(new ProductModels.Type() { Name = "Vlees", Info = info2 });
            types1.Add(new ProductModels.Type() { Name = "Brood", Info = info3 });
            //Types 2
            List<Info> info4 = new();
            info4.Add(new Info() { Name = "Schermgrootte", Value = "" });
            info4.Add(new Info() { Name = "Processortype", Value = "" });
            info4.Add(new Info() { Name = "RAM-geheugen", Value = "" });
            info4.Add(new Info() { Name = "Merk", Value = "" });
            info4.Add(new Info() { Name = "Videokaart", Value = "" });
            info4.Add(new Info() { Name = "Capaciteit SSD", Value = "" });
            List<Info> info5 = new();
            info5.Add(new Info() { Name = "Schermgrootte", Value = "" });
            info5.Add(new Info() { Name = "Verversingssnelheid", Value = "" });
            info5.Add(new Info() { Name = "Beeldscherpte", Value = "" });
            info5.Add(new Info() { Name = "Merk", Value = "" });
            List<Info> info6 = new();
            info6.Add(new Info() { Name = "Schermdiagonaal", Value = "" });
            info6.Add(new Info() { Name = "Merk", Value = "" });
            info6.Add(new Info() { Name = "Resolutie", Value = "" });
            info6.Add(new Info() { Name = "Beeldverversing", Value = "" });
            info6.Add(new Info() { Name = "Introductiejaar", Value = "" });
            info6.Add(new Info() { Name = "Energielabel", Value = "" });
            List<Info> info7 = new();
            info7.Add(new Info() { Name = "Merk", Value = "" });
            info7.Add(new Info() { Name = "Videoresolutie", Value = "" });
            List<ProductModels.Type> types2 = new();
            types2.Add(new ProductModels.Type() { Name = "Laptops", Info = info4 });
            types2.Add(new ProductModels.Type() { Name = "Monitoren", Info = info5 });
            types2.Add(new ProductModels.Type() { Name = "Televisies", Info = info6 });
            types2.Add(new ProductModels.Type() { Name = "Cameras", Info = info7 });
            //Return categories
            return new List<Category>()
            {
                new Category()
                {
                    Id = "abcd2149e773f2a3990b47f5",
                    Name = "Voedsel",
                    Types = types1
                },
                new Category()
                {
                    Id = "abcd2149e773f2a3990b47f6",
                    Name = "Elektronica",
                    Types = types2
                },
            };
        }
    }
}
