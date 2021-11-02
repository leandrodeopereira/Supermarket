namespace SupermarketApi.RequestHandlers
{
    using OneOf;
    using static SupermarketApi.RequestHandlers.UpdateOrderPaymentStatusResponse;
    using Order = Entities.OrderAggregate.Order;

    public class UpdateOrderPaymentStatusResponse : OneOfBase<OrderPaymentStatusUpdated, OrderNotFound>
    {
        protected UpdateOrderPaymentStatusResponse(OneOf<OrderPaymentStatusUpdated, OrderNotFound> input) : base(input)
        {
        }

        public sealed class OrderNotFound { }

        public sealed class OrderPaymentStatusUpdated
        {
            public OrderPaymentStatusUpdated(Order order)
            {
                this.Order = order;
            }

            public Order Order { get; }
        }

        public static implicit operator UpdateOrderPaymentStatusResponse(OrderNotFound _) => new(_);

        public static implicit operator UpdateOrderPaymentStatusResponse(OrderPaymentStatusUpdated _) => new(_);
    }
}
