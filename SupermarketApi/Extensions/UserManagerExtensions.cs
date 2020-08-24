namespace SupermarketApi.Extensions
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Entities.Identity;

    public static class UserManagerExtensions
    {
        public static async Task<AppUser?> FindUserByClaimsPrincipleWithAddressAsync(
            this UserManager<AppUser>? userManager,
            ClaimsPrincipal claimsPrincipal)
        {
            if (userManager is null)
            {
                return default;
            }

            var email = claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email).ConfigureAwait(false);
        }

        public static async Task<AppUser?> FindByEmailFromClaimsPrincipleAsync(
            this UserManager<AppUser>? userManager,
            ClaimsPrincipal claimsPrincipal)
        {
            if (userManager is null)
            {
                return default;
            }

            var email = claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            return await userManager.Users.SingleOrDefaultAsync(x => x.Email == email).ConfigureAwait(false);
        }
    }
}
