﻿namespace SupermarketApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SupermarketApi.Entities.OrderAggregate;

    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);

        Task<IReadOnlyCollection<DeliveryMethod>> GetDeliveryMethodAsync();

        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);

        Task<IReadOnlyCollection<Order>> GetOrdersForUserAsync(string buyerEmail);
    }
}
