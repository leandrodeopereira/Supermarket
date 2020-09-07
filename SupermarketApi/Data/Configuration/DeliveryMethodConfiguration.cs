namespace SupermarketApi.Data.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SupermarketApi.Entities.OrderAggregate;

    [ExcludeFromCodeCoverage]
    public sealed class DeliveryMethodConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> builder)
        {
            _ = builder ?? throw new System.ArgumentNullException(nameof(builder));

            _ = builder.Property(d => d.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
