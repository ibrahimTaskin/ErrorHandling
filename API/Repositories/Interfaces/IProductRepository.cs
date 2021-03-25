using API.Application;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Result<IEnumerable<Product>>> GetProducts();
        Task<Result<Product>> GetProduct(string id); // Result tipinde geri dönüş istiyoruz.

        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}
