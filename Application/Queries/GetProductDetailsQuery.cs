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

    public class GetProductDetailsQuery : IRequest<ProductDetailsDto>
    {
        public long ProductId { get; set; }
    }
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsDto>
    {
        readonly IProductService _productService;
        IMapper _mapper;

        public GetProductDetailsQueryHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<ProductDetailsDto> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var productDetails = (await _productService.GetProductDetailsAsync()).Where(i=>i.ProductId == request.ProductId).FirstOrDefault();

            if (productDetails != null)
            {
                return productDetails;
            }
            return new ProductDetailsDto();
        }
    }
}
