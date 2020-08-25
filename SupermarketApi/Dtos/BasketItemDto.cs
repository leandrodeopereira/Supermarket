#nullable disable
namespace SupermarketApi.Dtos
{
    using System;

    public class BasketItemDto
    {
        public string Brand { get; set; }

        public int Id { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public Uri PictureUrl { get; set; }

        public int Quantity { get; set; }
    }
}
