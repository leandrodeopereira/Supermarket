namespace SupermarketApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Repositories;
    using Product = Entities.Product;

    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        async Task<CustomerBasket?> IPaymentService.CreateOrUpdatePaymentIntent(string basketId)
        {
            _ = basketId ?? throw new ArgumentNullException(nameof(basketId));

            StripeConfiguration.ApiKey = this.configuration["StripeSettings:SecretKey"];

            var basket = await this.basketRepository.GetBasketAsync(basketId);
            var shippingPrice = 0m;

            if (basket is null)
            {
                return default;
            }

            if (basket.DeliveryMehodId.HasValue)
            {
                var deliveryMethod = await this.unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMehodId);
                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in basket.Items)
            {
                var productItem = await this.unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                // HACK: Never thrust what comes from the client side.
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    // HACK: Stripe doesn't take decimals for the amount, it takes the number on the long format
                    // so we need to convert decimal to long, multiplying by 100.
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + ((long)shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    // HACK: Stripe doesn't take decimals for the amount, it takes the number on the long format
                    // so we need to convert decimal to long, multiplying by 100.
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + ((long)shippingPrice * 100),
                };

                _ = await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            _ = await this.basketRepository.SetBasketAsync(basket);

            return basket;
        }
    }
}
