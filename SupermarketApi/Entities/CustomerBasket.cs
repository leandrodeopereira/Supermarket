#nullable disable
namespace SupermarketApi.Entities
{
    using System.Collections.Generic;

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

        public IReadOnlyCollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
