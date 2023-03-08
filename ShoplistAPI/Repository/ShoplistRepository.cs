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
    }
}
