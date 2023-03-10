using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
using ShoplistAPI.Model;
using ShoplistAPI.Pagination;
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

        public async Task<PagedList<Shoplist>> GetShoplists(ShoplistParameters shoplistParameter)
        {
            return PagedList<Shoplist>.ToPagedList(
                 GetAll()
                .Select(sl => new Shoplist
                {
                    Id = sl.Id,
                    Name = sl.Name,
                    Description = sl.Description,
                    Products = sl.Products,
                }).AsNoTracking(),
                shoplistParameter.Page,
                shoplistParameter.PageSize);
        }
    }
}
