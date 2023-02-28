using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;

namespace ShoplistAPI.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ShoplistContext _context;

        public ProductRepository(ShoplistContext shoplistContext)
        {
            _context = shoplistContext;
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            return await _context.Products
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    Number = p.Number,
                    ShoplistId = p.ShoplistId
                })
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Add(Product product)
        {
            await _context.AddAsync(product);
            _context.SaveChanges();
            return product;
        }

        public async void Update(int id, Product product)
        {
            product.Id = id;
            _context.Update(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}
