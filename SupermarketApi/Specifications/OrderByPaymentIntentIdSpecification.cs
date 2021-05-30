namespace SupermarketApi.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SupermarketApi.Entities.OrderAggregate;

    public class OrderByPaymentIntentIdSpecification : ASpecWithQueryOperations<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId)
        {
            this.Expression = o => o.PaymentIntentId == paymentIntentId;
        }

        public override Expression<Func<Order, bool>> Expression { get; }
    }
}
