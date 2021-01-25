namespace SupermarketApi.RequestHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Repositories;
    using SupermarketApi.Services;
    using SupermarketApi.Specifications;
    using static SupermarketApi.RequestHandlers.CreateOrderResponse;

    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;
        private readonly IPaymentService paymentService;

        public CreateOrderRequestHandler(
            IUnitOfWork unitOfWork,
            IBasketRepository basketRepository,
            IPaymentService paymentService)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
            this.paymentService = paymentService;
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

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await this.unitOfWork.Repository<Product>().GetByIdAsync(item.Id).ConfigureAwait(false);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PicturePath);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await this.unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(request.DeliveryMethodId).ConfigureAwait(false);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var spec = new OrderByPaymentIntentIdWithItemsSpecification(basket.PaymentIntentId);
            var existingOrder = await this.unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (existingOrder is Order)
            {
                this.unitOfWork.Repository<Order>().Delete(existingOrder);
                _ = await this.paymentService.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            }

            var order = new Order(items, request.BuyerEmail, basket.PaymentIntentId, request.ShippingAddress, deliveryMethod, subtotal);

            this.unitOfWork.Repository<Order>().Add(order);

            var result = await this.unitOfWork.Complete();

            return result <= 0 ? new ErrorCreatingOrder() : (CreateOrderResponse)new OrderCreated(order);
        }
    }
}
