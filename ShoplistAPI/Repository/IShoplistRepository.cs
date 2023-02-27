using ShoplistAPI.Model;

namespace ShoplistAPI.Repository
{
    public interface IShoplistRepository
    {
        Task<List<Shoplist>> GetAll();
        Task<Shoplist> GetById(int id);
        Task<Shoplist> Add(Shoplist shoplist);
        Task<Shoplist> Update(int id, Shoplist shoplist);
        Task<bool> Delete(int id);
    }
}
