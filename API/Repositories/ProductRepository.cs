using API.Application;
using API.Data.Interfaces;
using API.Models;
using API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;
        public ProductRepository(IProductContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Product>>> GetProducts() // İstek Result tipinde olmalı
        {
            return Result<IEnumerable<Product>>.Success(await _context.Products.Find(p => true).ToListAsync()); // Geri dönüş de Result tipinde olmalı.
        }

        public async Task<Result<Product>> GetProduct(string id)
        {
            var product = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

            return Result<Product>.Success(product); // Succes ile gönderiyoruz.
        }

        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

    }
}
