namespace SupermarketApi.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SupermarketApi.Entities.OrderAggregate;

    internal sealed class OrdersWithItemsAndOrderingSpecification : ASpecWithQueryOperations<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(string email)
        {
            _ = email ?? throw new ArgumentNullException(nameof(email));

            this.Expression = o => o.BuyerEmail == email;

            this.AddInclude(o => o.OrderItems);
            this.AddInclude(o => o.DeliveryMethod);
            this.OrderByDescending = o => o.OrderDate;
        }

        public OrdersWithItemsAndOrderingSpecification(int id, string email)
        {
            _ = email ?? throw new ArgumentNullException(nameof(email));

            this.Expression = o => o.Id == id && o.BuyerEmail == email;

            this.AddInclude(o => o.OrderItems);
            this.AddInclude(o => o.DeliveryMethod);
        }

        public override Expression<Func<Order, bool>> Expression { get; }
    }
}
