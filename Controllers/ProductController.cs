using InventoryManagementAPI.Data;
using InventoryManagementAPI.Entities;
using InventoryManagementAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, Employee")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    QuantityInStock = p.QuantityInStock,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, Employee")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    QuantityInStock = p.QuantityInStock,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                return BadRequest("Product name is required.");
            }

            var category = await _context.Categories.FindAsync(productDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId.");
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                QuantityInStock = productDto.QuantityInStock,
                CategoryId = productDto.CategoryId,
                Description=productDto.Description
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                QuantityInStock = product.QuantityInStock,
                CategoryId = product.CategoryId,
                CategoryName = category.Name
            });
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            var category = await _context.Categories.FindAsync(productDto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId.");
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.QuantityInStock = productDto.QuantityInStock;
            product.CategoryId = productDto.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Product/Category/5
        [HttpGet("Category/{categoryId}")]
        [Authorize(Roles = "Admin, Manager, Employee")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound($"Category with ID {categoryId} not found.");
            }

            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    QuantityInStock = p.QuantityInStock,
                    CategoryId = p.CategoryId,
                    CategoryName = category.Name
                })
                .ToListAsync();

            return Ok(products);
        }
    }
}
