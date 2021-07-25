namespace SupermarketApi.RequestHandlers
{
    using System;
    using System.Collections.Generic;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;

    public sealed class OrderContext
    {
        public OrderContext(
            CustomerBasket basket,
            string buyerEmail,
            DeliveryMethod deliveryMethod,
            IEnumerable<Product> products,
            Address shippingAddress)
        {
            this.Basket = basket ?? throw new ArgumentNullException(nameof(basket));
            this.BuyerEmail = buyerEmail ?? throw new ArgumentNullException(nameof(buyerEmail));
            this.DeliveryMethod = deliveryMethod ?? throw new ArgumentNullException(nameof(deliveryMethod));
            this.Products = products ?? throw new ArgumentNullException(nameof(products));
            this.ShippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress));
        }

        public CustomerBasket Basket { get; }

        public string BuyerEmail { get; }

        public DeliveryMethod DeliveryMethod { get; }

        public IEnumerable<Product> Products { get; }

        public Address ShippingAddress { get; }
    }
}
