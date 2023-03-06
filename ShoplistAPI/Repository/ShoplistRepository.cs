using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;

namespace ShoplistAPI.Repository
{
    public class ShoplistRepository : IShoplistRepository
    {

        private readonly ShoplistContext _context;

        public ShoplistRepository(ShoplistContext shoplistContext)
        {
            _context = shoplistContext;
        }

        public async Task<List<ShoplistDTO>> GetAll()
        {
            return await _context.Shoplists
                .Select(sl => new ShoplistDTO
                {
                    Id = sl.Id,
                    Name = sl.Name,
                    Description = sl.Description,
                    Products = sl.Products,
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Shoplist> GetById(int id)
        {
            return await _context.Shoplists.Include(sl => sl.Products).FirstOrDefaultAsync(sl => sl.Id == id);
        }

        public async Task<Shoplist> Add(Shoplist shoplist)
        {
            await _context.AddAsync(shoplist);
            _context.SaveChanges();
            return shoplist;
        }

        public async void Update(int id, Shoplist shoplist)
        {
            shoplist.Id = id;
            _context.Update(shoplist);
            _context.SaveChanges();
        }

        public void Delete(Shoplist shoplist)
        {
            _context.Remove(shoplist);
            _context.SaveChanges();
        }
    }
}
