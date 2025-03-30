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
    public class GetOrderHistoryQuery : IRequest<IEnumerable<OrderDto>>
    {
        public GetOrderHistoryQuery() { }
        public long customerId { get; set; }
    }
    public class GetOrderHistoryQueryHandler : IRequestHandler<GetOrderHistoryQuery, IEnumerable<OrderDto>>
    {
        readonly IOrderService _orderService;
        IMapper _mapper;

        public GetOrderHistoryQueryHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderDto>> Handle(GetOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderService.GetOrdersWithItemsDto(request.customerId);

            if (orderList != null)
            {
                return orderList;
            }
            return new List<OrderDto>();
        }
    }
}
