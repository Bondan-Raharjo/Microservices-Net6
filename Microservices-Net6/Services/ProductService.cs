using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices_Net6.DTOs;
using Microservices_Net6.Models;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Services.Interfaces
{
    
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return MapToDtoList(products);
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? MapToDto(product) : null;
        }

        public async Task<IEnumerable<ProductDTO>> SearchByNameAsync(string name)
        {
            var products = await _productRepository.SearchByNameAsync(name);
            return MapToDtoList(products);
        }

        public async Task<IEnumerable<ProductDTO>> SearchByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.SearchByPriceRangeAsync(minPrice, maxPrice);
            return MapToDtoList(products);
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CreatedAt = DateTime.UtcNow
            };

            var createdProduct = await _productRepository.CreateAsync(product);
            return MapToDto(createdProduct);
        }

        public async Task<ProductDTO> UpdateAsync(int id, UpdateProductDTO productDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return null;

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;

            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
            return MapToDto(updatedProduct);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        private ProductDTO MapToDto(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt
            };
        }

        private IEnumerable<ProductDTO> MapToDtoList(IEnumerable<Product> products)
        {
            var productDtos = new List<ProductDTO>();

            foreach (var product in products)
            {
                productDtos.Add(MapToDto(product));
            }

            return productDtos;
        }
    }
}