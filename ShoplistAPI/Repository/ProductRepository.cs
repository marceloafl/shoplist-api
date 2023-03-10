using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;
using ShoplistAPI.Pagination;

namespace ShoplistAPI.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        private readonly ShoplistContext _context;

        public ProductRepository(ShoplistContext shoplistContext): base(shoplistContext)
        {
            _context = shoplistContext;
        }

        public async Task<PagedList<Product>> GetProducts(ProductParameters productParameters)
        {
            //return await GetAll()
            //    .Skip((productParameters.Page - 1) * productParameters.PageSize)
            //    .Take(productParameters.PageSize)
            //    .ToListAsync();

            return PagedList<Product>.ToPagedList(
                GetAll(),
                productParameters.Page,
                productParameters.PageSize);
        }
    }
}
