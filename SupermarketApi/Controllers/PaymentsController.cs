namespace SupermarketApi.Controllers
{
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Stripe;
    using SupermarketApi.Configuration;
    using SupermarketApi.Entities;
    using SupermarketApi.Errors;
    using SupermarketApi.RequestHandlers;
    using SupermarketApi.Services;
    using static Entities.OrderAggregate.OrderStatus;
    using Order = Entities.OrderAggregate.Order;

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IMediator mediator;
        private readonly StripeSettings stripeSettings;

        public PaymentsController(IPaymentService paymentService, IOptions<StripeSettings> options, IMediator mediator)
        {
            this.paymentService = paymentService;
            this.mediator = mediator;
            this.stripeSettings = options.Value;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> SavePaymentIntent(string basketId)
        {
            if (basketId is null)
            {
                return this.BadRequest("The 'basketId' cannot be null.");
            }

            var updatedBasket = await this.mediator.Send(new SavePaymentIntentRequest(basketId));

            return updatedBasket.Match<ActionResult<CustomerBasket>>(
                paymentIntentSaved => this.Ok(paymentIntentSaved.Basket),
                basketNotFound => this.BadRequest(new ApiResponse(HttpStatusCode.BadRequest, "Problem with your basket.")));
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(this.HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                this.Request.Headers["Stripe-Signature"],
                this.stripeSettings.WebhookSecret!);

            PaymentIntent intent;
            Order? order = default;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await this.paymentService.UpdateOrderPaymentStatus(intent.Id, PaymentReceived);
                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await this.paymentService.UpdateOrderPaymentStatus(intent.Id, PaymentFailed);
                    break;

                default:
                    break;
            }

            return order switch
            {
                { } => this.Ok(order),
                _ => this.NotFound(),
            };
        }
    }
}
