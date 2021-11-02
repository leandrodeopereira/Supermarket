namespace SupermarketApi.Mapping
{
    using SupermarketApi.Entities.OrderAggregate;
    using static Entities.OrderAggregate.OrderStatus;

    public class OrderStatusFromStripeEventTypeBuilder : IBuilder<string, OrderStatus>
    {
        OrderStatus IBuilder<string, OrderStatus>.Build(string input)
        {
            return input switch
            {
                "payment_intent.succeeded" => PaymentReceived,
                "payment_intent.payment_failed" => PaymentFailed,
                _ => PaymentUnknown
            };
        }
    }
}
