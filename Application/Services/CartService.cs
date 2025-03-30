using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly IIplfranchiseEcommDbContext _context;
        IMapper _mapper;

        public CartService(IIplfranchiseEcommDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<CustomerCartDto>> CreateCartAsync(CreateCartDto createCartDto)
        {
            var response = new ServiceResponse<CustomerCartDto>();

            try
            {
                // Validate customer exists
                var customerExists = await _context.Customers
                    .AnyAsync(c => c.CustomerId == createCartDto.CustomerId);

                if (!customerExists)
                {
                    response.Success = false;
                    response.Message = "Customer not found";
                    return response;
                }

                // Validate products exist and are active
                var productIds = createCartDto.Items.Select(i => i.ProductId).ToList();
                var products = await _context.Products
                    .Where(p => productIds.Contains(p.ProductId) && p.IsActive)
                    .ToListAsync();

                if (products?.Count != productIds.Count)
                {
                    response.Success = false;
                    response.Message = "One or more products are invalid or inactive";
                    return response;
                }

                // Check for existing active cart
                var existingCart = await _context.CustomerCarts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.CustomerId == createCartDto.CustomerId);


               // var cartItems = await _context.CustomerCarts 
                //    .FirstOrDefaultAsync(c => c.cus == createCartDto.CustomerId);

                if (existingCart != null)
                {
                    // Update existing cart
                    foreach (var itemDto in createCartDto.Items)
                    {
                        var existingItem = existingCart.CartItems
                            .FirstOrDefault(ci => ci.ProductId == itemDto.ProductId);

                        if (existingItem != null)
                        {
                            existingItem.Quantity += itemDto.Quantity;
                        }
                        else
                        {
                            var newItem = new CartItem
                            {
                                CartId = Convert.ToInt32(existingCart.CartId),
                                ProductId = itemDto.ProductId,
                                Quantity = itemDto.Quantity,
                                CreatedOn = DateTime.UtcNow,
                                ProductSizeId=itemDto.ProductSizeID
                            };
                             _context.CartItems.Add(newItem);
                        }
                    }

                    await _context.SaveChangesAsync();
                    //response.Data = _mapper.Map<CustomerCartDto>(existingCart);
                }
                else
                {
                    // Create new cart with items
                    //using var transaction = await _context.Database.BeginTransactionAsync();
                    try
                    {
                        var newCart = new CustomerCart
                        {
                            CustomerId = createCartDto.CustomerId,
                            CreatedOn = DateTime.UtcNow
                        };

                        await _context.CustomerCarts.AddAsync(newCart);
                        await _context.SaveChangesAsync();

                        var cartItems = createCartDto.Items.Select(item => new CartItem
                        {
                            CartId =  Convert.ToInt32(newCart.CartId),
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            CreatedOn = DateTime.UtcNow
                        }).ToList();

                        await _context.CartItems.AddRangeAsync(cartItems);
                        await _context.SaveChangesAsync();

                        //await transaction.CommitAsync();

                        // Load the complete cart with items

                       var CustomerCart= await _context.CustomerCarts
                            .Include(c => c.CartItems)
                                .ThenInclude(ci => ci.Product)
                            .FirstOrDefaultAsync(c => c.CartId == newCart.CartId);

                        //response.Data =_mapper.Map<CustomerCartDto>(CustomerCart);
                    }
                    catch
                    {
                        //await transaction.RollbackAsync();
                        throw;
                    }
                }

                response.Success = true;
                response.Message = "Cart created successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the cart";
                response.Error = ex.Message;
            }

            return response;
        }
    }
}

