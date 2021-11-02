namespace SupermarketApi.Entities.OrderAggregate
{
    using System.Runtime.Serialization;

    public enum OrderStatus
    {
        [EnumMember(Value = "Payment Unknown")]
        PaymentUnknown,

        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed,
    }
}
