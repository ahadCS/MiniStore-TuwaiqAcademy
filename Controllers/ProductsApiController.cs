using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mini_store.Data;
using mini_store.Models;
using Microsoft.AspNetCore.Authorization;

namespace mini_store.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController] 
    public class ProductsApiController : ControllerBase {
        private readonly AppDbContext _context;

        public ProductsApiController(AppDbContext context)
        {
            _context = context;
        }





        //GET /api/ProductsApi
          [HttpGet("products")]
       
        public async Task<IActionResult> GetProducts([FromQuery] string? searchTerm)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            var products = await query.ToListAsync();
            return Ok(products); // يعيد البيانات كـ JSON نقي لـ Swagger ولتطبيقات الجوال
        }

         [HttpGet("categories")]
       
        public async Task<IActionResult> GetCategories([FromQuery] string? searchTerm)
        {
            var query = await _context.Categories.ToListAsync();
            return Ok(query); // يعيد البيانات كـ JSON نقي لـ Swagger ولتطبيقات الجوال
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new { message = $"المنتج ذو الرقم {id} غير موجود" });
            }

            return Ok(product);
        }

        // : POST /api/ProductsApi
        
    }
}