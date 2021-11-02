namespace SupermarketApi.RequestHandlers
{
    using System;
    using MediatR;
    using SupermarketApi.Entities.OrderAggregate;

    public class UpdateOrderPaymentStatusRequest : IRequest<UpdateOrderPaymentStatusResponse>
    {
        public UpdateOrderPaymentStatusRequest(string paymentIntentId, OrderStatus orderStatus)
        {
            this.PaymentIntentId = paymentIntentId ?? throw new ArgumentNullException(nameof(paymentIntentId));
            this.OrderStatus = orderStatus;
        }

        public string PaymentIntentId { get; }

        public OrderStatus OrderStatus { get; }
    }
}
