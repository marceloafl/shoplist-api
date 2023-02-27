using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
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

        public async Task<List<Shoplist>> GetAll()
        {
            return await _context.Shoplists.ToListAsync();
        }

        public async Task<Shoplist> GetById(int id)
        {
            return await _context.Shoplists.FirstOrDefaultAsync(sl => sl.Id == id);
        }

        public async Task<Shoplist> Add(Shoplist shoplist)
        {
            await _context.Shoplists.AddAsync(shoplist);
            await _context.SaveChangesAsync();
            return shoplist;
        }

        public async Task<Shoplist> Update(int id, Shoplist shoplist)
        {
            var shoplistWithQueriedId = await GetById(id);

            if (shoplistWithQueriedId == null)
            {
                throw new Exception($"Não foi possível encontrar lista de compras com o ID {id}.");
            }

            shoplistWithQueriedId.Name = shoplist.Name;
            shoplistWithQueriedId.Description = shoplist.Description;
            shoplistWithQueriedId.Products = shoplist.Products;

            _context.Shoplists.Update(shoplistWithQueriedId);
            await _context.SaveChangesAsync();

            return shoplistWithQueriedId;
        }

        public async Task<bool> Delete(int id)
        {
            var shoplistWithQueriedId = await GetById(id);

            if (shoplistWithQueriedId == null)
            {
                throw new Exception($"Não foi possível encontrar lista de compras com o ID {id}.");
            }

            _context.Shoplists.Remove(shoplistWithQueriedId);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
