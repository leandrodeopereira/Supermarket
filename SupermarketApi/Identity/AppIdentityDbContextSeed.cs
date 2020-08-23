namespace SupermarketApi.Identity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Internal;
    using SupermarketApi.Entities.Identity;

    [ExcludeFromCodeCoverage]
    public sealed class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            _ = userManager ?? throw new ArgumentNullException(nameof(userManager));

            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Leandro",
                    Email = "leandro@test.com",
                    UserName = "leandro@test.com",
                    Address = new Address
                    {
                        FirstName = "Leandro",
                        LastName = "Pereira",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210",
                    },
                };

                _ = await userManager.CreateAsync(user, "Pa$$w0rd").ConfigureAwait(false);
            }
        }
    }
}
