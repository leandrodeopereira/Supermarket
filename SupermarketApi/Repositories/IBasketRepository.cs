namespace SupermarketApi.Repositories
{
    using System.Threading.Tasks;
    using SupermarketApi.Entities;

    public interface IBasketRepository
    {
        Task<bool> DeleteBasketAsync(string basketId);

        Task<CustomerBasket?> GetBasketAsync(string basketId);

        Task<CustomerBasket?> SetBasketAsync(CustomerBasket basket);
    }
}
