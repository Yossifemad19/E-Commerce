using AutoMapper;
using E_Commerce.Api.DTOs;
using E_Commerce.Api.Errors;
using E_Commerce.Api.Extensions;
using E_Commerce.Core.Entities.OrdderAggregate;
using E_Commerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    public class OrderController:BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [HttpGet("deliveryMethods")]

        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }


        [Authorize]
        [HttpPost("createOrder")]

        public async Task<ActionResult<Order>> createOrder(OrderDto orderForm)
        {
            var email = HttpContext.User?.GetUserEmail();

            var address = _mapper.Map<AddressDto, Address>(orderForm.shippingAddress);
            var order = await _orderService.CreateOrderAsync(email,address, orderForm.deliveryMethodId, orderForm.baskeId);

            if (order is null)
            {
                return BadRequest(new ApiResponse(400,"Proplem creating order"));
            }

            return Ok(order);
        }

        [Authorize]
        [HttpGet("getOrder")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var email= HttpContext.User.GetUserEmail();
            
            var order =await _orderService.GetOrderByIdAsync(id, email);
            
            if (order is null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [Authorize]
        [HttpGet("AllOrders")]

        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GettAllOrders()
        {
            var email=HttpContext.User.GetUserEmail();

            var orders =await _orderService.GetOrdersAsync(email);

            if (orders is null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }
    }
}
