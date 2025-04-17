using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microservices_Net6.DTOs;
using Microservices_Net6.Services.Interfaces;
using Microservices_Net6.Repositories.Interfaces;

namespace Microservices_Net6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require authentication for all product endpoints
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            _logger.LogInformation("Getting all products");
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            _logger.LogInformation("Getting product with ID: {Id}", id);
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Search term cannot be empty");

            _logger.LogInformation("Searching products by name: {Name}", name);
            var products = await _productService.SearchByNameAsync(name);
            return Ok(products);
        }

        [HttpGet("price-range")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> SearchByPriceRange(
            [FromQuery] decimal minPrice = 0,
            [FromQuery] decimal maxPrice = decimal.MaxValue)
        {
            if (minPrice < 0)
                return BadRequest("Minimum price cannot be negative");

            if (maxPrice < minPrice)
                return BadRequest("Maximum price cannot be less than minimum price");

            _logger.LogInformation("Searching products by price range: {MinPrice} - {MaxPrice}", minPrice, maxPrice);
            var products = await _productService.SearchByPriceRangeAsync(minPrice, maxPrice);
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admins can create products
        public async Task<ActionResult<ProductDTO>> Create([FromBody] CreateProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Creating new product: {Name}", productDto.Name);
            var createdProduct = await _productService.CreateAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can update products
        public async Task<ActionResult<ProductDTO>> Update(int id, [FromBody] UpdateProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Updating product with ID: {Id}", id);
            var updatedProduct = await _productService.UpdateAsync(id, productDto);
            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can delete products
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);
            var result = await _productService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}