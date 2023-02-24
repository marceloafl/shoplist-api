using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data;
using ShoplistAPI.Model;
using System.Diagnostics;

namespace ShoplistAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoplistController: ControllerBase
    {
        private ShoplistContext _context;

        public ShoplistController(ShoplistContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Shoplist> GetAll()
        {
            return _context.Shoplists.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var shoplistWithQueriedId = _context.Shoplists.FirstOrDefault(shoplist => shoplist.Id == id);
            if (shoplistWithQueriedId == null) return NotFound();

            return Ok(shoplistWithQueriedId);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Shoplist shoplist)
        {
            _context.Shoplists.Add(shoplist);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById),
                new { id = shoplist.Id },
                shoplist);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Shoplist shoplist)
        {
            var shoplistWithQueriedId = _context.Shoplists.FirstOrDefault(shoplist => shoplist.Id == id);
            if (shoplistWithQueriedId == null) return NotFound();

            shoplistWithQueriedId.Name = shoplist.Name;
            shoplistWithQueriedId.Description = shoplist.Description;
            shoplistWithQueriedId.Products = shoplist.Products;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShoplist(int id)
        {
            var shoplistWithQueriedId = _context.Shoplists.FirstOrDefault(shoplist => shoplist.Id == id);
            if (shoplistWithQueriedId == null) return NotFound();

            _context.Remove(shoplistWithQueriedId);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
