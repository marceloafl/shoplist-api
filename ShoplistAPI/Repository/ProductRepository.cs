using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data;
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

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(int id, Product product)
        {
            var productWithQueriedId = await GetById(id);

            if (productWithQueriedId == null)
            {
                throw new Exception($"Não foi possível encontrar produto com o ID {id}.");
            }

            productWithQueriedId.Name = product.Name;
            productWithQueriedId.Brand = product.Brand;
            productWithQueriedId.Description = product.Description;
            productWithQueriedId.Number = product.Number;
            productWithQueriedId.ShoplistId = product.ShoplistId;

            _context.Products.Update(productWithQueriedId);
            await _context.SaveChangesAsync();

            return productWithQueriedId;
        }

        public async Task<bool> Delete(int id)
        {
            var productWithQueriedId = await GetById(id);

            if (productWithQueriedId == null)
            {
                throw new Exception($"Não foi possível encontrar produto com o ID {id}.");
            }

            _context.Products.Remove(productWithQueriedId);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
