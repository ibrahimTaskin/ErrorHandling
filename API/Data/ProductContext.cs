using API.Data.Interfaces;
using API.Models;
using API.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            Products = database.GetCollection<Product>(databaseSettings.CollectionName);
            ProductContextSeed.Seed(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
