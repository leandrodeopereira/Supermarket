namespace SupermarketApi.Data.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SupermarketApi.Entities.OrderAggregate;

    [ExcludeFromCodeCoverage]
    public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            _ = builder.OwnsOne(i => i.ItemOrdered, io => io.WithOwner());
            _ = builder.Property(i => i.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
