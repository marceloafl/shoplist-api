using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;

namespace ShoplistAPI.Repository
{
    public class ShoplistRepository : Repository<Shoplist>, IShoplistRepository
    {

        private readonly ShoplistContext _context;

        public ShoplistRepository(ShoplistContext shoplistContext): base(shoplistContext)
        {
            _context = shoplistContext;
        }

        //public async Task<List<ShoplistDTO>> GetAll()
        //{
        //    return await _context.Shoplists
        //        .Select(sl => new ShoplistDTO
        //        {
        //            Id = sl.Id,
        //            Name = sl.Name,
        //            Description = sl.Description,
        //            Products = sl.Products,
        //        })
        //        .AsNoTracking()
        //        .ToListAsync();
        //}
    }
}
