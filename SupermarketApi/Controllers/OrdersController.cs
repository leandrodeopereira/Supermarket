namespace SupermarketApi.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities.OrderAggregate;
    using SupermarketApi.Errors;
    using SupermarketApi.Extensions;
    using SupermarketApi.Services;

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public sealed class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = this.HttpContext.User.RetrieveEmailFromPrincipal();

            if (email is null)
            {
                return this.BadRequest(new ApiResponse(HttpStatusCode.NotFound, "Email not found!"));
            }

            var address = this.mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await this.orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address).ConfigureAwait(false);

            return order is null
                ? this.BadRequest(new ApiResponse(HttpStatusCode.BadRequest, "Problem creating order"))
                : (ActionResult<Order>)this.Ok(order);
        }
    }
}
