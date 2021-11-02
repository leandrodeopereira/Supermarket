namespace SupermarketApi.RequestHandlers
{
    using OneOf;
    using SupermarketApi.Entities;
    using static SupermarketApi.RequestHandlers.SavePaymentIntentResponse;

    public class SavePaymentIntentResponse : OneOfBase<PaymentIntentSaved, BasketNotFound>
    {
        protected SavePaymentIntentResponse(OneOf<PaymentIntentSaved, BasketNotFound> input) : base(input)
        {
        }

        public sealed class BasketNotFound { }

        public sealed class PaymentIntentSaved
        {
            public PaymentIntentSaved(CustomerBasket basket)
            {
                this.Basket = basket;
            }

            public CustomerBasket Basket { get; }
        }

        public static implicit operator SavePaymentIntentResponse(BasketNotFound _) => new(_);

        public static implicit operator SavePaymentIntentResponse(PaymentIntentSaved _) => new(_);
    }
}
