namespace SupermarketApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Specifications;

    public sealed class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
