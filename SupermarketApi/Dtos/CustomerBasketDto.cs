#nullable disable
namespace SupermarketApi.Dtos
{
    using System.Collections.Generic;

    public class CustomerBasketDto
    {
        public string ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }

        public string Id { get; set; }

        public IReadOnlyCollection<BasketItemDto> Items { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}
