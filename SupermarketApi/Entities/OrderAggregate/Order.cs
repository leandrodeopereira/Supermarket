#nullable disable
namespace SupermarketApi.Entities.OrderAggregate
{
    using System;
    using System.Collections.Generic;
    using static OrderStatus;

    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(
            IReadOnlyList<OrderItem> orderItems,
            string buyerEmail,
            Address shipToAddress,
            DeliveryMethod deliveryMethod,
            decimal subtotal)
        {
            this.OrderItems = orderItems;
            this.BuyerEmail = buyerEmail;
            this.DeliveryMethod = deliveryMethod;
            this.ShipToAddress = shipToAddress;
            this.Subtotal = subtotal;
        }

        public string BuyerEmail { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public OrderStatus Status { get; set; } = Pending;

        public string PaymentIntentId { get; set; }

        public Address ShipToAddress { get; set; }

        public decimal Subtotal { get; set; }

        public decimal GetTotal()
        {
            return this.Subtotal + this.DeliveryMethod.Price;
        }
    }
}
