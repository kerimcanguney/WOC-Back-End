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
            info1.Add(new Info() { Name = "Land van herkomst", Value = "Nederland" });
            List<Info> info2 = new();
            info2.Add(new Info() { Name = "Calorieen", Value = "10kcal" });
            info2.Add(new Info() { Name = "Gewicht", Value = "100g" });
            info2.Add(new Info() { Name = "Land van herkomst", Value = "Costa Rica" });
            List<Info> info3 = new();
            info3.Add(new Info() { Name = "Calorieen", Value = "10kcal" });
            info3.Add(new Info() { Name = "Gewicht", Value = "100g" });
            info3.Add(new Info() { Name = "Houdbaarheid", Value = "5 dagen" });
            List<Info> info4 = new();
            info4.Add(new Info() { Name = "Producent", Value = "Bakker Tom" });
            info4.Add(new Info() { Name = "Houdbaarheid", Value = "2 dagen" });

            List<Info> info5 = new();
            info5.Add(new Info() { Name = "Schermgrootte", Value = "17.3 inch" });
            info5.Add(new Info() { Name = "Processortype", Value = "Intel Core i7" });
            info5.Add(new Info() { Name = "RAM-geheugen", Value = "16 GB" });
            info5.Add(new Info() { Name = "Merk", Value = "HP" });
            info5.Add(new Info() { Name = "Videokaart", Value = "NVIDIA GeForce MX450" });
            info5.Add(new Info() { Name = "Capaciteit SSD", Value = "512 GB" });
            List<Info> info6 = new();
            info6.Add(new Info() { Name = "Schermgrootte", Value = "15.6 inch" });
            info6.Add(new Info() { Name = "Processortype", Value = "AMD Ryzen 3" });
            info6.Add(new Info() { Name = "RAM-geheugen", Value = "8 GB" });
            info6.Add(new Info() { Name = "Merk", Value = "HP" });
            info6.Add(new Info() { Name = "Videokaart", Value = "AMD Radeon Graphics" });
            info6.Add(new Info() { Name = "Capaciteit SSD", Value = "256 GB" });
            List<Info> info7 = new();
            info7.Add(new Info() { Name = "Schermgrootte", Value = "15.6 inch" });
            info7.Add(new Info() { Name = "Processortype", Value = "AMD Ryzen 5" });
            info7.Add(new Info() { Name = "RAM-geheugen", Value = "8 GB" });
            info7.Add(new Info() { Name = "Merk", Value = "Lenovo" });
            info7.Add(new Info() { Name = "Videokaart", Value = "NVIDIA GeForce GTX 1650" });
            info7.Add(new Info() { Name = "Capaciteit SSD", Value = "512 GB" });
            List<Info> info8 = new();
            info8.Add(new Info() { Name = "Schermgrootte", Value = "24 inch" });
            info8.Add(new Info() { Name = "Verversingssnelheid", Value = "75 Hz" });
            info8.Add(new Info() { Name = "Beeldscherpte", Value = "Full HD" });
            info8.Add(new Info() { Name = "Merk", Value = "Samsung" });
            List<Info> info9 = new();
            info9.Add(new Info() { Name = "Schermdiagonaal", Value = "55 inch" });
            info9.Add(new Info() { Name = "Merk", Value = "Samsung" });
            info9.Add(new Info() { Name = "Resolutie", Value = "4K UHD" });
            info9.Add(new Info() { Name = "Beeldverversing", Value = "100 Hz" });
            info9.Add(new Info() { Name = "Introductiejaar", Value = "2021" });
            info9.Add(new Info() { Name = "Energielabel", Value = "G" });
            List<Info> info10 = new();
            info10.Add(new Info() { Name = "Merk", Value = "Crosstour" });
            info10.Add(new Info() { Name = "Videoresolutie", Value = "4k" });
            return new List<Product>()
            {
                //Voedsel
                new Product()
                {
                    Id = "62974dede769eb700731cdc7",
                    Name = "Appel",
                    Category = "Voedsel",
                    Type = "Fruit",
                    Info = info1
                },
                new Product()
                {
                    Id = "62974df5e769eb700731cdc8",
                    Name = "Banaan",
                    Category = "Voedsel",
                    Type = "Fruit",
                    Info = info2
                },
                new Product()
                {
                    Id = "62974df5e769eb700731cdc9",
                    Name = "Ham",
                    Category = "Voedsel",
                    Type = "Vlees",
                    Info = info3
                },
                new Product()
                {
                    Id = "62974df5e769eb700731cdca",
                    Name = "Wit brood",
                    Category = "Voedsel",
                    Type = "Brood",
                    Info = info4
                },
                // Elektronica
                new Product()
                {
                    Id = "62974df5e769eb700731cdcb",
                    Name = "HP ENVY 17-ch1720nd",
                    Category = "Elektronica",
                    Type = "Laptops",
                    Info = info5
                },
                new Product()
                {
                    Id = "62974df5e769eb700731cdcc",
                    Name = "HP 15s-eq1711nd",
                    Category = "Elektronica",
                    Type = "Laptops",
                    Info = info6
                },
                new Product()
                {
                    Id = "62974df5e769eb700731cdcd",
                    Name = "Lenovo IdeaPad Gaming 3 15ACH6 82K200L4MH",
                    Category = "Elektronica",
                    Type = "Laptops",
                    Info = info7
                },
                new Product()
                {
                    Id = "62974df5e769eb700731cdce",
                    Name = "Samsung LS24R350",
                    Category = "Elektronica",
                    Type = "Monitoren",
                    Info = info8
                },
                new Product()
                {
                    Id = "62974df5e769eb700731cdcf",
                    Name = "Samsung QE55Q80A",
                    Category = "Elektronica",
                    Type = "Televisies",
                    Info = info9
                },
                new Product()
                {
                    Id = "62974e01e769eb700731cdd0",
                    Name = "Crosstour Sport Action Camera",
                    Category = "Elektronica",
                    Type = "Cameras",
                    Info = info10
                },
            };
        }
    }
}
