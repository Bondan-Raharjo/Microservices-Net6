using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> SearchByNameAsync(string name);
        Task<IEnumerable<ProductDTO>> SearchByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<ProductDTO> CreateAsync(CreateProductDTO productDto);
        Task<ProductDTO> UpdateAsync(int id, UpdateProductDTO productDto);
        Task<bool> DeleteAsync(int id);
    }
}