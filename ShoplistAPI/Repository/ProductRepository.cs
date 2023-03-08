using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;

namespace ShoplistAPI.Repository
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        private readonly ShoplistContext _context;

        public ProductRepository(ShoplistContext shoplistContext): base(shoplistContext)
        {
            _context = shoplistContext;
        }
    }
}
