using Application.Interfaces;
using Application.Models;
using Application.Types;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Types.EnumTypes;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IIplfranchiseEcommDbContext _context;
        IMapper _mapper;

        public OrderService(IIplfranchiseEcommDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<OrderDto>> CreateOrderFromCartAsync(long customerId, long cartId)
        {
            var response = new ServiceResponse<OrderDto>();

            try
            {
                // Get cart with items
                var cart = (await _context.CustomerCarts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.CartId == cartId && c.CustomerId == customerId));

                if (cart == null || !cart.CartItems.Any())
                {
                    response.Success = false;
                    response.Message = "Cart not found or empty";
                    return response;
                }

                // Calculate total amount
                decimal? totalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

                // Create order
                var order = new OrderDetail
                {
                    CustomerId = customerId,
                    OrderDate = DateTime.UtcNow,
                    OrderStatusId = (int)EnumTypes.OrderStatusTypes.Confirmed,
                    PaymentMethodId=(int)PaymentMethods.COD,
                    ProductPrice= totalAmount
                };

                await _context.OrderDetails.AddAsync(order);
                await _context.SaveChangesAsync();

                // Create order items
                var orderItems = cart.CartItems.Select(ci => new OrderDetailItem
                {
                    OrderId = order.OrderId,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductPrice = ci.Product.Price,
                }).ToList();

                await _context.OrderDetailItems.AddRangeAsync(orderItems);
                await _context.SaveChangesAsync();

                var cartItemIds = cart.CartItems.Select(ci => ci.CartItemId).ToList(); 
                IList <CartItem> removableCartItmLst  = _context.CartItems.Where(i => cartItemIds.Contains(i.CartItemId)).ToList();
                _context.CartItems.RemoveRange(removableCartItmLst);
                await _context.SaveChangesAsync();

                // Load complete order with items
                //var completeOrder = await _context.Orders
                //    .Include(o => o.Customer)
                //    .Include(o => o.OrderItems)
                //        .ThenInclude(oi => oi.Product)
                //            .ThenInclude(p => p.Iplfranchise)
                //    .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

                //response.Data = completeOrder;
                response.Success = true;
                response.Message = "Order created successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the order";
                response.Error = ex.Message;
                return response;
            }
        }

        //public async Task<List<OrderDetail>> GetOrdersWithItems(long cusomerId)
        //{
        //    var orders = await _context.OrderDetails.Where(o => o.CustomerId == cusomerId)
        //        .Include(o => o.OrderDetailItems)
        //            .ThenInclude(oi => oi.Product)  
        //        .Include(o => o.OrderStatus)        
        //        .Include(o => o.PaymentMethod)     
        //        .ToListAsync();

        //    return orders;
        //}

        public async Task<List<OrderDto>> GetOrdersWithItemsDto(long cusomerId)
        {
            var orders = await _context.OrderDetails
                .Where(i=>i.CustomerId== cusomerId)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    OrderDate = o.OrderDate,
                    OrderStatusId = o.OrderStatusId,
                    PaymentMethodId = o.PaymentMethodId,
                    ProductPrice = o.ProductPrice
                })
                .ToListAsync();

            foreach (var order in orders)
            { 
                order.OrderItems = await _context.OrderDetailItems
                    .Where(oi => oi.OrderId == order.OrderId)
                    .Select(oi => new OrderDetailItemDto
                    {
                        OrderItemId = oi.OrderItemId,
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        ProductPrice = oi.ProductPrice,
                        ProductName = oi.Product.ProductName
                    })
                    .ToListAsync();

            }

            return orders;
        }

    }
}
