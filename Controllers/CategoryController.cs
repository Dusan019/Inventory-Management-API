using InventoryManagementAPI.Data;
using InventoryManagementAPI.Entities;
using InventoryManagementAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Management_API.Dto.CategoryDto;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, Employee")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, Employee")]
        public async Task<ActionResult<CategoryWithProductsDto>> GetCategory(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryWithProductsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products.Select(p => new CategorySpecificProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        QuantityInStock = p.QuantityInStock
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            if (string.IsNullOrEmpty(categoryDto.Name))
            {
                return BadRequest("Category name is required.");
            }

            var category = new Category
            {
                Name = categoryDto.Name
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryDto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("{id}/Products")]
        [Authorize(Roles = "Admin, Manager, Employee")]
        public async Task<ActionResult<IEnumerable<CategorySpecificProductDto>>> GetProductsInCategory(int id)
        {
            // Fetch the category and include its products
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    Products = c.Products.Select(p => new CategorySpecificProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        QuantityInStock = p.QuantityInStock
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            // Check if the category exists
            if (category == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            // Return the products in the category
            return Ok(category.Products);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
