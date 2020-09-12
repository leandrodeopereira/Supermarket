namespace SupermarketApi.Controllers
{
    using System.Collections.Generic;
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

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyCollection<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await this.orderService.GetDeliveryMethodAsync();

            return this.Ok(deliveryMethods);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = this.HttpContext.User.RetrieveEmailFromPrincipal();

            if (email is null)
            {
                return this.BadRequest(new ApiResponse(HttpStatusCode.NotFound, "Email not found!"));
            }

            var orders = await this.orderService.GetOrdersForUserAsync(email);

            return this.Ok(this.mapper.Map<IReadOnlyCollection<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = this.HttpContext.User.RetrieveEmailFromPrincipal();

            if (email is null)
            {
                return this.BadRequest(new ApiResponse(HttpStatusCode.NotFound, "Email not found!"));
            }

            var order = await this.orderService.GetOrderByIdAsync(id, email);

            return order is null
                ? this.NotFound(new ApiResponse(HttpStatusCode.NotFound, "Order not found."))
                : (ActionResult<OrderToReturnDto>)this.Ok(this.mapper.Map<OrderToReturnDto>(order));
        }
    }
}
