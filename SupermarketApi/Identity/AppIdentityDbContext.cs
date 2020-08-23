namespace SupermarketApi.Identity
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Entities.Identity;

    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            base.OnModelCreating(builder);
        }
    }
}
