using Application.Interfaces;
using Application.Models;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateCartCommand : IRequest<ServiceResponse<CustomerCartDto>>
    {
        public CreateCartDto createCartDto { get; set; }
    }
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, ServiceResponse<CustomerCartDto>>
    {
        readonly ICartService _cartService;
        IMapper _mapper;

        public CreateCartCommandHandler(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<CustomerCartDto>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = (await _cartService.CreateCartAsync(request.createCartDto));
            return serviceResponse;
        }
    }
}
