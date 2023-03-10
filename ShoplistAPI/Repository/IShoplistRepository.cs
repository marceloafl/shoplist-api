using ShoplistAPI.Model;
using ShoplistAPI.Pagination;

namespace ShoplistAPI.Repository
{
    public interface IShoplistRepository : IRepository<Shoplist>
    {
        Task<PagedList<Shoplist>> GetShoplists(ShoplistParameters shoplistParameter);
    }
}
