namespace SupermarketApi.Services
{
    using System.Threading.Tasks;
    using SupermarketApi.Entities.OrderAggregate;

    public interface IPaymentService
    {
        Task<Order?> UpdateOrderPaymentStatus(string paymentIntentId, OrderStatus orderStatus);
    }
}
