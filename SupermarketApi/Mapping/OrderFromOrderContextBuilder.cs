namespace SupermarketApi.Mapping
{
    using System.Collections.Generic;
    using System.Linq;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.RequestHandlers;

    public sealed class OrderFromOrderContextBuilder : IBuilder<OrderContext, Order>
    {
        Order IBuilder<OrderContext, Order>.Build(OrderContext input)
        {
            _ = input ?? throw new System.ArgumentNullException(nameof(input));

            var items = new List<OrderItem>();
            foreach (var item in input.Basket.Items)
            {
                var productItem = input.Products.Single(p => p.Id == item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PicturePath);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            return new Order(
                items,
                input.BuyerEmail,
                input.Basket.PaymentIntentId,
                input.ShippingAddress,
                input.DeliveryMethod,
                subtotal);
        }
    }
}
