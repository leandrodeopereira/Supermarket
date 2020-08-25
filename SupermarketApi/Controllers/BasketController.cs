namespace SupermarketApi.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities;
    using SupermarketApi.Errors;
    using SupermarketApi.Mapping;
    using SupermarketApi.Repositories;

    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder;
        private readonly IMapper mapper;

        public BasketController(
            IBasketRepository basketRepository,
            IBuilder<HttpStatusCode, ApiResponse> apiResponseBuilder,
            IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.apiResponseBuilder = apiResponseBuilder;
            this.mapper = mapper;
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            _ = await this.basketRepository.DeleteBasketAsync(id).ConfigureAwait(false);
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await this.basketRepository.GetBasketAsync(id).ConfigureAwait(false);

            // HACK: Always returns a basket to the user even if
            // there it does not exist in the database.
            return this.Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var updated = await this.basketRepository
                .SetBasketAsync(this.mapper.Map<CustomerBasket>(basket))
                .ConfigureAwait(false);

            return updated switch
            {
                { } => this.Ok(basket),
                _ => this.NotFound(this.apiResponseBuilder.Build(HttpStatusCode.NotFound)),
            };
        }
    }
}
