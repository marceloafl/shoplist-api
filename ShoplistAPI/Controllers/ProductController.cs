using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data;
using ShoplistAPI.Model;

namespace ShoplistAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private ShoplistContext _context;

        public ProductController(ShoplistContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var productWithQueriedId = _context.Products.FirstOrDefault(product => product.Id == id);
            if (productWithQueriedId == null) return NotFound();

            return Ok(productWithQueriedId);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),
                new { id = product.Id },
                product);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            var productWithQueriedId = _context.Products.FirstOrDefault(product => product.Id == id);
            if (productWithQueriedId == null) return NotFound();

            productWithQueriedId.Name = product.Name;
            productWithQueriedId.Brand = product.Brand;
            productWithQueriedId.Description = product.Description;
            productWithQueriedId.Number = product.Number;
            productWithQueriedId.ShoplistId = product.ShoplistId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productWithQueriedId = _context.Products.FirstOrDefault(product => product.Id == id);
            if (productWithQueriedId == null) return NotFound();

            _context.Remove(productWithQueriedId);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
