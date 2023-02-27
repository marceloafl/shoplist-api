using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoplistAPI.Model;

namespace ShoplistAPI.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__product__3213E83F59140FE6");

            builder.ToTable("product");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Brand)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("brand");
            builder.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            builder.Property(e => e.Number).HasColumnName("number");
            builder.Property(e => e.ShoplistId).HasColumnName("shoplistId");

            builder.HasOne(d => d.Shoplist).WithMany(p => p.Products)
                .HasForeignKey(d => d.ShoplistId)
                .HasConstraintName("FK__product__shoplis__267ABA7A");
        }
    }
}
