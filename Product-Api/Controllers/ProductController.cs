using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("GetProducts")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Set<Product>().ToListAsync();
    }
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

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Set<Product>().Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = product.id }, product);
    }

}