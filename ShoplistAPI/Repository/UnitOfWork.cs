using ShoplistAPI.Data;

namespace ShoplistAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShoplistRepository _shoplistRepo;
        private ProductRepository _productRepo;
        public ShoplistContext _context;

        public UnitOfWork(ShoplistContext context)
        {
            _context = context;
        }

        public IShoplistRepository ShoplistRepository
        {
            get
            {
                return _shoplistRepo = _shoplistRepo ?? new ShoplistRepository(_context);
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepo = _productRepo ?? new ProductRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
