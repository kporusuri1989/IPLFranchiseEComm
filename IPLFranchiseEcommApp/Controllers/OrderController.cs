using Application.Commands;
using Application.Models;
using Application.Queries;
using AutoMapper;
using IPLFranchiseEcommApp.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IMediator _mediator;
        IMapper _mapper;
        public OrderController(ILogger<OrderController> logger, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [OrderOperation(5)]
        [Route("CreateOrder")]
        public async Task<ActionResult<ServiceResponse<CustomerCartDto>>> CreateOrder([FromBody] CreateOrderRequestBody objRequestBody)
        {
            try
            {
                var res = await _mediator.Send(
                    new CreateOrderCommand()
                    {
                        CutomerId= objRequestBody.CustomerId,
                        CartId= objRequestBody.CartId
                    });
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the order");
            }
        }


        [HttpGet]
        [OrderOperation(6)]
        [Route("OrderHistory")]
        public async Task<ActionResult<List<ProductDetailsDto>>> GetOrderHistory([FromQuery] long CustomerId)
        {
            try
            {
                var ordersList = await _mediator.Send(new GetOrderHistoryQuery() { customerId = CustomerId });
                return Ok(ordersList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving ordersList");
            }
        }
    }
}
