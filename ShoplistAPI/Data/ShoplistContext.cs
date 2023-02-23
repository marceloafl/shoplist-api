using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShoplistAPI.Model;

namespace ShoplistAPI.Data;

public partial class ShoplistContext : DbContext
{
    public ShoplistContext()
    {
    }

    public ShoplistContext(DbContextOptions<ShoplistContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shoplist> Shoplists { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=shoplist;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83F59140FE6");

            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.ShoplistId).HasColumnName("shoplistId");

            entity.HasOne(d => d.Shoplist).WithMany(p => p.Products)
                .HasForeignKey(d => d.ShoplistId)
                .HasConstraintName("FK__product__shoplis__267ABA7A");
        });

        modelBuilder.Entity<Shoplist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__shoplist__3213E83F313C83C3");

            entity.ToTable("shoplist");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
