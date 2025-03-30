using Application.Interfaces;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetProductsOnSearchQuery : IRequest<List<ProductDetailsDto>>
    {
        public ProductSearchDto SearchDto { get; set; }
        //public string? ProductName { get; set; }
        //public string? ProductCode { get; set; }
        //public string? Franchise { get; set; }
        //public string? Category { get; set; }
        //public decimal? minPrice { get; set; }
        //public decimal? maxPrice { get; set; }
    }
    public class GetProductsOnSearchQueryHandler : IRequestHandler<GetProductsOnSearchQuery, List<ProductDetailsDto>>
    {
        readonly IProductService _productService;
        IMapper _mapper;

        public GetProductsOnSearchQueryHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<List<ProductDetailsDto>> Handle(GetProductsOnSearchQuery request, CancellationToken cancellationToken)
        {
            //var productSearchDtoObj = _mapper.Map<ProductSearchDto>(request);

            var productDetails = (await _productService.SearchProductsAsync(request.SearchDto)).ToList();

            if (productDetails != null)
            {
                return productDetails;
            }
            return new List<ProductDetailsDto>();
        }
    }
}
