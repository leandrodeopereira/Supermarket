namespace SupermarketApi.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SupermarketApi.Entities.OrderAggregate;

    public class OrderByPaymentIntentIdWithItemsSpecification : ASpecWithQueryOperations<Order>
    {
        public OrderByPaymentIntentIdWithItemsSpecification(string paymentIntentId)
        {
            this.Expression = o => o.PaymentIntentId == paymentIntentId;
        }

        public override Expression<Func<Order, bool>> Expression { get; }
    }
}
