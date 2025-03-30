using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIplfranchiseEcommDbContext
    {
         DbSet<Address> Addresses { get; set; }

         DbSet<CartItem> CartItems { get; set; }

         DbSet<Category> Categories { get; set; }

         DbSet<Customer> Customers { get; set; }

         DbSet<CustomerCart> CustomerCarts { get; set; }

         DbSet<Iplfranchise> Iplfranchises { get; set; }

         DbSet<OrderDetail> OrderDetails { get; set; }

         DbSet<OrderDetailItem> OrderDetailItems { get; set; }

         DbSet<OrderStatus> OrderStatuses { get; set; }

         DbSet<PaymentMethod> PaymentMethods { get; set; }

         DbSet<Product> Products { get; set; }

         DbSet<ProductSize> ProductSizes { get; set; }

         DbSet<ProductSizeAvailability> ProductSizeAvailabilities { get; set; }

         DbSet<Tenant> Tenants { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
