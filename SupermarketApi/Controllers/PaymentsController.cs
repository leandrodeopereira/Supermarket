namespace SupermarketApi.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SupermarketApi.Entities;
    using SupermarketApi.Errors;
    using SupermarketApi.Mapping;
    using SupermarketApi.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private readonly IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder;

        public PaymentsController(IPaymentService paymentService, IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder)
        {
            this.paymentService = paymentService;
            this.apiResponseBuilder = apiResponseBuilder;
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
                _ => this.NotFound(this.apiResponseBuilder.Build(HttpStatusCode.NotFound)),
            };
        }
    }
}
