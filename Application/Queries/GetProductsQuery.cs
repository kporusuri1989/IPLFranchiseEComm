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
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public GetProductsQuery() { }
    }
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        readonly IProductService _productService;
        IMapper _mapper;

        public GetProductsQueryHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IList<ProductDto>>((await _productService.GetProductDetailsAsync()).ToList());

            if (products != null)
            {
                return products;
            }
            return new List<ProductDto>();
        }
    }
}
