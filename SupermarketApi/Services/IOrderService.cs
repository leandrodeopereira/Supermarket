namespace SupermarketApi.Services
{
    using System.Threading.Tasks;
    using SupermarketApi.Entities.OrderAggregate;

    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
    }
}
