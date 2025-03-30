using Application.Interfaces;
using Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateOrderCommand : IRequest<ServiceResponse<OrderDto>>
    {
        public long CartId {  get; set; }
        public long CutomerId {  get; set; }
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<OrderDto>>
    {
        readonly IOrderService _orderService;
        IMapper _mapper;

        public CreateOrderCommandHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var serviceResponse = (await _orderService.CreateOrderFromCartAsync(request.CutomerId,request.CartId));
            return serviceResponse;
        }
    }
}
