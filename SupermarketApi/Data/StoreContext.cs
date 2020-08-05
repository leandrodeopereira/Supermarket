namespace SupermarketApi.Data
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Entities;

    [ExcludeFromCodeCoverage]
    public sealed class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
            this.Products = this.Set<Product>();
        }

        public DbSet<Product> Products { get; set; }
    }
}
