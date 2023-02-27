using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoplistAPI.Model;

namespace ShoplistAPI.Data.Mappings
{
    public class ShoplistMap : IEntityTypeConfiguration<Shoplist>
    {
        public void Configure(EntityTypeBuilder<Shoplist> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__shoplist__3213E83F313C83C3");

            builder.ToTable("shoplist");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        }
    }
}
