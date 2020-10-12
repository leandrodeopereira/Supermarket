#nullable disable
namespace SupermarketApi.Dtos
{
    using System;
    using System.Collections.Generic;
    using SupermarketApi.Entities.OrderAggregate;

    public sealed class OrderToReturnDto
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public string DeliveryMethod { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public OrderStatus Status { get; set; }

        public decimal ShippingPrice { get; set; }

        public Address ShipToAddress { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }
    }
}
