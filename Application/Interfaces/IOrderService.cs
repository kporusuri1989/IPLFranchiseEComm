using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderDto>> CreateOrderFromCartAsync(long customerId, long cartId);
        Task<List<OrderDto>> GetOrdersWithItemsDto(long cusomerId);
    }
}
