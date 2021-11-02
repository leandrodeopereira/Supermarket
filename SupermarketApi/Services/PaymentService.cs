namespace SupermarketApi.Services
{
    using System.Threading.Tasks;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Specifications;
    using Order = Entities.OrderAggregate.Order;

    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        async Task<Order?> IPaymentService.UpdateOrderPaymentStatus(string paymentIntentId, OrderStatus orderStatus)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);

            var order = await this.unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order is null)
            {
                return default;
            }

            order.Status = orderStatus;

            this.unitOfWork.Repository<Order>().Update(order);

            _ = await this.unitOfWork.Complete();

            return order;
        }
    }
}
