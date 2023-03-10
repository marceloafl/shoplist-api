using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Data.Mappings;
using ShoplistAPI.Model;

namespace ShoplistAPI.Data;

public partial class ShoplistContext : DbContext
{
    public ShoplistContext(DbContextOptions<ShoplistContext> options): base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shoplist> Shoplists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ShoplistMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
    }

}
