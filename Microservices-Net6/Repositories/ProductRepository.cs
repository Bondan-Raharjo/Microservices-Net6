using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Repositories
{
    
    public class ProductRepository : IProductRepository
    {
        // Dummy data
        private static readonly List<Product> _products = new()
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High-performance laptop with SSD and 16GB RAM",
                Price = 1299.99m,
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new Product
            {
                Id = 2,
                Name = "Smartphone",
                Description = "Latest smartphone with high-resolution camera",
                Price = 799.99m,
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Product
            {
                Id = 3,
                Name = "Tablet",
                Description = "Lightweight tablet with long battery life",
                Price = 499.99m,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new Product
            {
                Id = 4,
                Name = "Desktop PC",
                Description = "Gaming desktop with high-end graphics card",
                Price = 1599.99m,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            }
        };

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Product>>(_products);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            var products = _products.Where(p =>
                p.Name.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                (p.Description != null && p.Description.Contains(name, StringComparison.OrdinalIgnoreCase))
            );
            return Task.FromResult(products);
        }

        public Task<IEnumerable<Product>> SearchByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = _products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
            return Task.FromResult(products);
        }

        public Task<Product> CreateAsync(Product product)
        {
            var maxId = _products.Count > 0 ? _products.Max(p => p.Id) : 0;
            product.Id = maxId + 1;
            product.CreatedAt = DateTime.UtcNow;
            _products.Add(product);
            return Task.FromResult(product);
        }

        public Task<Product> UpdateAsync(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                return Task.FromResult(existingProduct);
            }
            return Task.FromResult<Product>(null);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}