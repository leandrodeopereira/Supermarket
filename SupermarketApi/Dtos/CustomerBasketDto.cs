#nullable disable
namespace SupermarketApi.Dtos
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class CustomerBasketDto
    {
        public string Id { get; set; }

        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "It is a DTO")]
        public IReadOnlyCollection<BasketItemDto> Items { get; set; }
    }
}
