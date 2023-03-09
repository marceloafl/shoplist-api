using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using System.Linq.Expressions;

namespace ShoplistAPI.Repository
{
    public class ShoplistRepository : Repository<Shoplist>, IShoplistRepository
    {

        private readonly ShoplistContext _context;

        public ShoplistRepository(ShoplistContext shoplistContext): base(shoplistContext)
        {
            _context = shoplistContext;
        }

        public async Task<Shoplist> GetById(Expression<Func<Shoplist, bool>> predicate)
        {
            return await _context.Set<Shoplist>().AsNoTracking().Include(sl => sl.Products).SingleOrDefaultAsync(predicate);
        }
    }
}
