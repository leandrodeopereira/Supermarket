namespace SupermarketApi.Data
{
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Entities;

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
