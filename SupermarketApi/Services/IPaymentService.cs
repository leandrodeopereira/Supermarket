namespace SupermarketApi.Services
{
    using System.Threading.Tasks;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;

    public interface IPaymentService
    {
        Task<CustomerBasket?> SavePaymentIntent(string basketId);

        Task<Order?> UpdateOrderPaymentStatus(string paymentIntentId, OrderStatus orderStatus);
    }
}
