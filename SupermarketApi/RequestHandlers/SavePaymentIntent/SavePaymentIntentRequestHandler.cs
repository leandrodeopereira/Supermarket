namespace SupermarketApi.RequestHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Options;
    using Stripe;
    using SupermarketApi.Configuration;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Repositories;
    using SupermarketApi.Services;
    using static SupermarketApi.RequestHandlers.SavePaymentIntentResponse;
    using Product = Entities.Product;

    public class SavePaymentIntentRequestHandler : IRequestHandler<SavePaymentIntentRequest, SavePaymentIntentResponse>
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly StripeSettings stripeSettings;

        public SavePaymentIntentRequestHandler(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IOptions<StripeSettings> options)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.stripeSettings = options.Value;
        }

        async Task<SavePaymentIntentResponse> IRequestHandler<SavePaymentIntentRequest, SavePaymentIntentResponse>.Handle(
            SavePaymentIntentRequest request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            StripeConfiguration.ApiKey = this.stripeSettings.SecretKey!;

            var basket = await this.basketRepository.GetBasketAsync(request.BasketId);
            var shippingPrice = 0m;

            if (basket is null)
            {
                return new BasketNotFound();
            }

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await this.unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMethodId);
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

            return new PaymentIntentSaved(basket);
        }
    }
}
