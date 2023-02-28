using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;

namespace ShoplistAPI.Repository
{
    public interface IShoplistRepository
    {
        Task<List<ShoplistDTO>> GetAll();
        Task<Shoplist> GetById(int id);
        Task<Shoplist> Add(Shoplist shoplist);
        void Update(int id, Shoplist shoplist);
        void Delete(Shoplist shoplist);
    }
}
