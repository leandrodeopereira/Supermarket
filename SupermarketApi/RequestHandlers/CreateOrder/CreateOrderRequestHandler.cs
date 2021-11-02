namespace SupermarketApi.RequestHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Mapping;
    using SupermarketApi.Repositories;
    using SupermarketApi.Services;
    using SupermarketApi.Specifications;
    using static SupermarketApi.RequestHandlers.CreateOrderResponse;

    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;
        private readonly IPaymentService paymentService;
        private readonly IBuilder<OrderContext, Order> orderFromOrderContextBuilder;

        public CreateOrderRequestHandler(
            IUnitOfWork unitOfWork,
            IBasketRepository basketRepository,
            IPaymentService paymentService,
            IBuilder<OrderContext, Order> orderFromOrderContextBuilder)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
            this.paymentService = paymentService;
            this.orderFromOrderContextBuilder = orderFromOrderContextBuilder;
        }

        async Task<CreateOrderResponse> IRequestHandler<CreateOrderRequest, CreateOrderResponse>.Handle(
            CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var basket = await this.basketRepository.GetBasketAsync(request.BasketId).ConfigureAwait(false);

            if (basket is null)
            {
                return new BasketNotFound();
            }

            var productItems = new List<Product>();
            foreach (var item in basket.Items)
            {
                productItems.Add(await this.unitOfWork.Repository<Product>().GetByIdAsync(item.Id).ConfigureAwait(false));
            }

            var deliveryMethod = await this.unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(request.DeliveryMethodId).ConfigureAwait(false);

            var orderContext = new OrderContext(basket, request.BuyerEmail, deliveryMethod, productItems, request.ShippingAddress);

            var order = this.orderFromOrderContextBuilder.Build(orderContext);

            var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var existingOrder = await this.unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (existingOrder is Order)
            {
                this.unitOfWork.Repository<Order>().Delete(existingOrder);
                _ = await this.paymentService.SavePaymentIntent(basket.PaymentIntentId);
            }

            this.unitOfWork.Repository<Order>().Add(order);

            var result = await this.unitOfWork.Complete();

            return result <= 0 ? new ErrorCreatingOrder() : new OrderCreated(order);
        }
    }
}
