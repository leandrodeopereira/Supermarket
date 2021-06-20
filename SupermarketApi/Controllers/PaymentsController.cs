namespace SupermarketApi.Controllers
{
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using SupermarketApi.Entities;
    using SupermarketApi.Errors;
    using SupermarketApi.Services;
    using Order = Entities.OrderAggregate.Order;
    using static Entities.OrderAggregate.OrderStatus;
    using Microsoft.Extensions.Options;
    using SupermarketApi.Configuration;

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly StripeSettings stripeSettings;

        public PaymentsController(IPaymentService paymentService, IOptions<StripeSettings> options)
        {
            this.paymentService = paymentService;
            this.stripeSettings = options.Value;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            if (basketId is null)
            {
                return this.BadRequest("The 'basketId' cannot be null.");
            }

            var updatedBasket = await this.paymentService.CreateOrUpdatePaymentIntent(basketId);

            return updatedBasket switch
            {
                { } => this.Ok(updatedBasket),
                _ => this.BadRequest(new ApiResponse(HttpStatusCode.BadRequest, "Problem with your basket.")),
            };
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
