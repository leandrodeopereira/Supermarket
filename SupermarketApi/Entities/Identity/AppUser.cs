#nullable disable
namespace SupermarketApi.Entities.Identity
{
    using Microsoft.AspNetCore.Identity;

    public sealed class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public Address Address { get; set; }
    }
}
