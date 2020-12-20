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

    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Order?>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;

        public CreateOrderRequestHandler(
            IUnitOfWork unitOfWork,
            IBasketRepository basketRepository)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
        }

        async Task<Order?> IRequestHandler<CreateOrderRequest, Order?>.Handle(
            CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var basket = await this.basketRepository.GetBasketAsync(request.BasketId).ConfigureAwait(false);

            if (basket is null)
            {
                return default;
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

            var order = new Order(items, request.BuyerEmail, request.ShippingAddress, deliveryMethod, subtotal);

            this.unitOfWork.Repository<Order>().Add(order);

            var result = await this.unitOfWork.Complete();

            if (result <= 0)
            {
                return default;
            }

            _ = await this.basketRepository.DeleteBasketAsync(request.BasketId).ConfigureAwait(false);

            return order;
        }
    }
}
