﻿namespace SupermarketApi.Controllers
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
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Errors;
    using SupermarketApi.Mapping;
    using SupermarketApi.RequestHandlers;

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly StripeSettings stripeSettings;
        private readonly IBuilder<string, OrderStatus> builder;

        public PaymentsController(
            IOptions<StripeSettings> options,
            IMediator mediator,
            IBuilder<string, OrderStatus> builder)
        {
            this.mediator = mediator;
            this.builder = builder;
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

            var intent = (PaymentIntent)stripeEvent.Data.Object;
            var updateOrderPaymentStatusResponse = await this.mediator.Send(new UpdateOrderPaymentStatusRequest(intent.Id, this.builder.Build(stripeEvent.Type)));

            return updateOrderPaymentStatusResponse.Match<ActionResult>(
                orderPaymentStatusUpdated => this.Ok(updateOrderPaymentStatusResponse),
                OrderNotFound => this.NotFound());
        }
    }
}
