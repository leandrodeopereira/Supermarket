#nullable disable
namespace SupermarketApi.Entities
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public sealed class CustomerBasket
    {
        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }

        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "For now, it will be used as a dto too.")]
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
