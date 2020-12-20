namespace SupermarketApi.RequestHandlers
{
    using OneOf;
    using SupermarketApi.Entities.OrderAggregate;
    using static CreateOrderResponse;

    public class CreateOrderResponse : OneOfBase<OrderCreated, BasketNotFound, ErrorCreatingOrder>
    {
        protected CreateOrderResponse(OneOf<OrderCreated, BasketNotFound, ErrorCreatingOrder> input) : base(input)
        {
        }

        public sealed class BasketNotFound { }

        public sealed class ErrorCreatingOrder { }

        public sealed class OrderCreated
        {
            public OrderCreated(Order order)
            {
                this.Order = order;
            }

            public Order Order { get; }
        }

        public static implicit operator CreateOrderResponse(BasketNotFound _) => new CreateOrderResponse(_);

        public static implicit operator CreateOrderResponse(ErrorCreatingOrder _) => new CreateOrderResponse(_);

        public static implicit operator CreateOrderResponse(OrderCreated _) => new CreateOrderResponse(_);
    }
}
