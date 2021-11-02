namespace SupermarketApi.RequestHandlers
{
    using System;
    using MediatR;

    public class SavePaymentIntentRequest : IRequest<SavePaymentIntentResponse>
    {
        public SavePaymentIntentRequest(string basketId)
        {
            this.BasketId = basketId ?? throw new ArgumentNullException(nameof(basketId));
        }

        public string BasketId { get; set; }
    }
}
