namespace SupermarketApi.Data.Configuration
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SupermarketApi.Entities;

    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            _ = builder.Property(p => p.Id).IsRequired();
            _ = builder.Property(p => p.Name).HasMaxLength(100);
            _ = builder.Property(p => p.Description).HasMaxLength(180);
            _ = builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            _ = builder.Property(p => p.PictureUrl).IsRequired();
            _ = builder.HasOne(b => b.ProductBrand).WithMany()
                .HasForeignKey(p => p.ProductBrandId);
            _ = builder.HasOne(b => b.ProductType).WithMany()
                .HasForeignKey(p => p.ProductTypeId);
        }
    }
}
