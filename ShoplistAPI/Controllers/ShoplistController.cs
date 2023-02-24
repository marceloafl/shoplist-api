using Microsoft.AspNetCore.Mvc;
using ShoplistAPI.Data;
using ShoplistAPI.Model;

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
    }
}
