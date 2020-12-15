namespace SupermarketApi.Services
{
    using System.Threading.Tasks;
    using SupermarketApi.Entities;

    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
    }
}
