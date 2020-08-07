namespace SupermarketApi.Data
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Entities;

    [ExcludeFromCodeCoverage]
    public sealed class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => this.Set<Product>();

        public DbSet<ProductBrand> ProductBrands => this.Set<ProductBrand>();

        public DbSet<ProductType> ProductTypes => this.Set<ProductType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder ?? throw new System.ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
