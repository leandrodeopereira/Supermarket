namespace SupermarketApi.Data
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;

    [ExcludeFromCodeCoverage]
    public sealed class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public DbSet<DeliveryMethod> DeliveryMethods => this.Set<DeliveryMethod>();

        public DbSet<Order> Orders => this.Set<Order>();

        public DbSet<OrderItem> OrdersItems => this.Set<OrderItem>();

        public DbSet<Product> Products => this.Set<Product>();

        public DbSet<ProductBrand> ProductBrands => this.Set<ProductBrand>();

        public DbSet<ProductType> ProductTypes => this.Set<ProductType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder ?? throw new System.ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Managing Sqlite limitation with decimal type
            if (this.Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

                    foreach (var property in properties)
                    {
                        _ = modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion<double>();
                    }
                }
            }
        }
    }
}
