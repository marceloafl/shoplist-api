using ShoplistAPI.Model;

namespace ShoplistAPI.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Add(Product product);
        Task<Product> Update(int id, Product product);
        Task<bool> Delete(int id);
    }
}
