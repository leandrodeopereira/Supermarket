namespace SupermarketApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Repositories;
    using SupermarketApi.Specifications;

    public sealed class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
        }

        async Task<Order?> IOrderService.CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            _ = buyerEmail ?? throw new ArgumentNullException(nameof(buyerEmail));
            _ = basketId ?? throw new ArgumentException(nameof(basketId));
            _ = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress));

            var basket = await this.basketRepository.GetBasketAsync(basketId).ConfigureAwait(false);

            var items = new List<OrderItem>();
            foreach (var item in basket!.Items)
            {
                var productItem = await this.unitOfWork.Repository<Product>().GetByIdAsync(item.Id).ConfigureAwait(false);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PicturePath);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var deliveryMethod = await this.unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId).ConfigureAwait(false);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

            this.unitOfWork.Repository<Order>().Add(order);

            var result = await this.unitOfWork.Complete();

            if (result <= 0)
            {
                return default;
            }

            _ = await this.basketRepository.DeleteBasketAsync(basketId).ConfigureAwait(false);

            return order;
        }

        async Task<IReadOnlyCollection<DeliveryMethod>> IOrderService.GetDeliveryMethodAsync()
        {
            return await this.unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

        async Task<Order> IOrderService.GetOrderByIdAsync(int id, string buyerEmail)
        {
            _ = buyerEmail ?? throw new ArgumentNullException(nameof(buyerEmail));

            var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await this.unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        async Task<IReadOnlyCollection<Order>> IOrderService.GetOrdersForUserAsync(string buyerEmail)
        {
            _ = buyerEmail ?? throw new ArgumentNullException(nameof(buyerEmail));

            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await this.unitOfWork.Repository<Order>().GetAsync(spec);
        }
    }
}
