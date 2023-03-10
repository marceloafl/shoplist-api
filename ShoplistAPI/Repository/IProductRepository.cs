using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Pagination;

namespace ShoplistAPI.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetProducts(ProductParameters productParameters);
    }
}
