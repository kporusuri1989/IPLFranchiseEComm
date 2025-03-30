using Application.Commands;
using Application.Models;
using Application.Queries;
using AutoMapper;
using IPLFranchiseEcommApp.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        IMediator _mediator;
        IMapper _mapper;
        public CartController(ILogger<CartController> logger, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

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
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the cart");
            }
        } 
    }
}
