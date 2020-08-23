namespace SupermarketApi.Services
{
    using SupermarketApi.Entities.Identity;

    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
