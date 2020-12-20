namespace SupermarketApi.RequestHandlers
{
    using System;
    using MediatR;
    using SupermarketApi.Entities.OrderAggregate;

    public class CreateOrderRequest : IRequest<Order>
    {
        public CreateOrderRequest(
            string basketId,
            string buyerEmail,
            int deliveryMethodId,
            Address shippingAddress)
        {
            this.BuyerEmail = buyerEmail ?? throw new ArgumentNullException(nameof(buyerEmail));
            this.DeliveryMethodId = deliveryMethodId;
            this.BasketId = basketId ?? throw new ArgumentNullException(nameof(basketId));
            this.ShippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress));
        }

        public string BasketId { get; set; }

        public string BuyerEmail { get; set; }

        public int DeliveryMethodId { get; set; }

        public Address ShippingAddress { get; set; }
    }
}
