using API;
using Application.Models;
using Application.Queries;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using IPLFranchiseEcommApp.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;

namespace IPLFranchiseEcommApp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductsController :ControllerBase
    {
        IMediator _mediator ;
        IMapper _mapper;
        public ProductsController(ILogger<ProductsController> logger, IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [OrderOperation(1)]
        [Route("GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            try
            {
                var products = await _mediator.Send(new GetProductsQuery());

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving products");
            }
        }

        [HttpGet]
        [OrderOperation(2)]
        [Route("GetProductDetailsByID")]
        public async Task<ActionResult<List<ProductDetailsDto>>> GetProductDetailsByID([FromQuery] int ProductID)
        {
            try
            {
                var products = await _mediator.Send(new GetProductDetailsQuery() { ProductId=ProductID});
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving products");
            }
        }


        [HttpGet]
        [OrderOperation(2)]
        [Route("SearchProducts")]
        public async Task<ActionResult<List<ProductDetailsDto>>> SearchProducts([FromQuery] string? ProductName ,string? CategoryName, string? FranchiseName,int? MinPrice, int? MaxPrice)
        {
            try
            {
                var products = await _mediator.Send(
                    new GetProductsOnSearchQuery()
                    {
                        SearchDto = new ProductSearchDto
                        { 
                            ProductName= ProductName,
                            Category = CategoryName,
                            FranchiseName = FranchiseName,
                            minPrice = MinPrice,
                            maxPrice= MaxPrice
                        }
                    });
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving products");
            }
        }
    }
}
