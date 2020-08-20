namespace SupermarketApi.Repositories
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using StackExchange.Redis;
    using SupermarketApi.Entities;

    internal sealed class BasketRepository : IBasketRepository
    {
        private readonly IDatabase database;

        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            this.database = connectionMultiplexer.GetDatabase();
        }

        Task<bool> IBasketRepository.DeleteBasketAsync(string basketId)
        {
            _ = basketId ?? throw new ArgumentNullException(nameof(basketId));

            return CreateTask();

            async Task<bool> CreateTask()
            {
                return await this.database.KeyDeleteAsync(basketId).ConfigureAwait(false);
            }
        }

        Task<CustomerBasket?> IBasketRepository.GetBasketAsync(string basketId)
        {
            _ = basketId ?? throw new ArgumentNullException(nameof(basketId));

            return CreateTask();

            async Task<CustomerBasket?> CreateTask()
            {
                var data = await this.database.StringGetAsync(basketId).ConfigureAwait(false);

                return !data.IsNullOrEmpty ? JsonSerializer.Deserialize<CustomerBasket>(data) : null;
            }
        }

        Task<CustomerBasket?> IBasketRepository.SetBasketAsync(CustomerBasket basket)
        {
            _ = basket ?? throw new ArgumentNullException(nameof(basket));

            return CreateTask();

            async Task<CustomerBasket?> CreateTask()
            {
                var isCreated = await this.database
                    .StringSetAsync(
                        basket.Id,
                        JsonSerializer.Serialize(basket),
                        TimeSpan.FromDays(30)).ConfigureAwait(false);

                return isCreated ? basket : null;
            }
        }
    }
}
