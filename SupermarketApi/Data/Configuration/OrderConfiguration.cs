namespace SupermarketApi.Data.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SupermarketApi.Entities.OrderAggregate;

    [ExcludeFromCodeCoverage]
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        void IEntityTypeConfiguration<Order>.Configure(EntityTypeBuilder<Order> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            _ = builder.OwnsOne(o => o.ShipToAddress, a => a.WithOwner());
            _ = builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));

            _ = builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
