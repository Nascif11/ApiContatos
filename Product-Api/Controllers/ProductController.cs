using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Api.Model;

namespace Product_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Product/GetProducts
    [HttpGet("GetProducts")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Set<Product>().ToListAsync();
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Set<Product>().FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Set<Product>().Add(product);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = product.id },
            product
        );
    }

    // PUT: api/Product/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.id)
        {
            return BadRequest();
        }

        var existingProduct = await _context.Set<Product>().FindAsync(id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.ProductName = product.ProductName;
        existingProduct.Price = product.Price;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Product/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Set<Product>().FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _context.Set<Product>().Remove(product);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}