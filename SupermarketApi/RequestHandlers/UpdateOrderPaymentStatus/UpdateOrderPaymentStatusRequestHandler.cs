namespace SupermarketApi.RequestHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using SupermarketApi.Services;
    using SupermarketApi.Specifications;
    using static SupermarketApi.RequestHandlers.UpdateOrderPaymentStatusResponse;
    using Order = Entities.OrderAggregate.Order;

    public class UpdateOrderPaymentStatusRequestHandler : IRequestHandler<UpdateOrderPaymentStatusRequest, UpdateOrderPaymentStatusResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateOrderPaymentStatusRequestHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        async Task<UpdateOrderPaymentStatusResponse> IRequestHandler<UpdateOrderPaymentStatusRequest, UpdateOrderPaymentStatusResponse>.Handle(
            UpdateOrderPaymentStatusRequest request, CancellationToken cancellationToken)
        {
            var spec = new OrderByPaymentIntentIdSpecification(request.PaymentIntentId);

            var order = await this.unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order is null)
            {
                return new OrderNotFound();
            }

            order.Status = request.OrderStatus;

            this.unitOfWork.Repository<Order>().Update(order);

            _ = await this.unitOfWork.Complete();

            return new OrderPaymentStatusUpdated(order);
        }
    }
}
