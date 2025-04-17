using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices_Net6.Models;

namespace Microservices_Net6.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> SearchByNameAsync(string name);
        Task<IEnumerable<Product>> SearchByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}