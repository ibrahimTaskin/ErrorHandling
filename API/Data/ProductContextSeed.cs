using API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public static class ProductContextSeed
    {
        public static void Seed(IMongoCollection<Product> products)
        {
            bool existProduct = products.Find(p => true).Any(); // Eğer hiç ürün yoksa True döner

            if (!existProduct)
            {
                products.InsertManyAsync(GetPreProducts());
            }
        }

        private static IEnumerable<Product> GetPreProducts()
        {
            return new List<Product>() {
                new Product() { Name="T-Shirt" },
                new Product() { Name="Pants"},
                new Product() { Name="Tv" },
                new Product() { Name="Phones"},
                new Product() { Name="Pc" },
            };
        }
    }
}
