using Application.Commands;
using Application.Models;
using Application.Queries;
using AutoMapper;
using IPLFranchiseEcommApp.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        IMediator _mediator;
        IMapper _mapper;
        ILogger<CartController> _logger;
        public CartController(ILogger<CartController> logger, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// AddItemsToCart
        /// </summary>
        /// <param name="objRequestBody"></param>
        /// <returns></returns>
        [HttpPost]
        [OrderOperation(4)]
        [Route("AddItemsToCart")]
        public async Task<ActionResult<ServiceResponse<CustomerCartDto>>> AddItemsToCart([FromBody] CreateCartRequestBody objRequestBody)
        {
            try
            {
                var res = await _mediator.Send(
                    new CreateCartCommand()
                    {
                        createCartDto = _mapper.Map<CreateCartDto>(objRequestBody)
                    });
                _logger.LogInformation("Cart created successfully for customer {0}", objRequestBody.CustomerId );
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the cart");

                return StatusCode(500, "An error occurred while creating the cart");
            }
        } 
    }
}
