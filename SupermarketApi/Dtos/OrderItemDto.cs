#nullable disable
namespace SupermarketApi.Dtos
{
    using System;

    public sealed class OrderItemDto
    {
        public Uri PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}
