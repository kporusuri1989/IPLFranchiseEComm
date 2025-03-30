using System;
using System.Collections.Generic;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public partial class IplfranchiseEcommDbContext : DbContext, IIplfranchiseEcommDbContext
    {
    public IplfranchiseEcommDbContext()
    {
    }

    public IplfranchiseEcommDbContext(DbContextOptions<IplfranchiseEcommDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerCart> CustomerCarts { get; set; }

    public virtual DbSet<Iplfranchise> Iplfranchises { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderDetailItem> OrderDetailItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<ProductSizeAvailability> ProductSizeAvailabilities { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:IPLECommerceDB");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AddLine1).HasMaxLength(500);
            entity.Property(e => e.AddLine2).HasMaxLength(500);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.State).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Customer");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("CartItem");

            entity.Property(e => e.ProductSizeId).HasColumnName("ProductSizeID");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK_CartItem_CustomerCart");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Product1");

            entity.HasOne(d => d.ProductSize).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductSizeId)
                .HasConstraintName("FK_CartItem_ProductSize");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .HasColumnName("EmailID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.PhoneNum).HasMaxLength(15);
        });

        modelBuilder.Entity<CustomerCart>(entity =>
        {
            entity.HasKey(e => e.CartId);

            entity.ToTable("CustomerCart");

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerCarts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerCart_Customer");
        });

        modelBuilder.Entity<Iplfranchise>(entity =>
        {
            entity.ToTable("IPLFranchise");

            entity.Property(e => e.IplfranchiseId).HasColumnName("IPLFranchiseID");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.IplteamName)
                .HasMaxLength(100)
                .HasColumnName("IPLTeamName");
            entity.Property(e => e.IsActive)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TenantId).HasColumnName("TenantID");

            entity.HasOne(d => d.Tenant).WithMany(p => p.Iplfranchises)
                .HasForeignKey(d => d.TenantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IPLFranchise_Tenant");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_OrderItems1");

            entity.ToTable("OrderDetail");

            //    entity.Property(e => e.OrderId).ValueGeneratedNever();
            //    entity.Property(e => e.CardNo).HasMaxLength(50);
            //    entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            //    entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");
            //    entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            //    entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 0)");

            //entity.HasOne(d => d.Customer).WithMany(p => p.OrderDetail)
            //    .HasForeignKey(d => d.CustomerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_OrderItems_Customer1");

            //entity.HasOne(d => d.OrderStatus).WithMany(p => p.OrderDetail)
            //    .HasForeignKey(d => d.OrderStatusId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_OrderItems_OrderStatus1");

            //entity.HasOne(d => d.PaymentMethod).WithMany(p => p.OrderDetail)
            //    .HasForeignKey(d => d.PaymentMethodId)
            //    .HasConstraintName("FK_OrderItems_PaymentMethod1");
        });

        modelBuilder.Entity<OrderDetailItem>(entity =>
            {
                entity.HasKey(e => e.OrderItemId).HasName("PK_OrderDetailItems_3");

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 0)");

                //entity.HasOne(d => d.OrderDetail).WithMany(p => p.OrderDetailItems)
                //    .HasForeignKey(d => d.OrderId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_OrderItems_Orders4");

                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetailItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItems_Product4");
            });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("OrderStatus");

            entity.Property(e => e.OrderStatusId)
                .ValueGeneratedNever()
                .HasColumnName("OrderStatusID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.OrderStatus1)
                .HasMaxLength(100)
                .HasColumnName("OrderStatus");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("PaymentMethod");

            entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.PaymentMethod1)
                .HasMaxLength(200)
                .HasColumnName("PaymentMethod");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IplfranchiseId).HasColumnName("IPLFranchiseID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductCode).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.TenantId).HasColumnName("TenantID");

            entity.HasOne(d => d.Categories).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.Iplfranchise).WithMany(p => p.Products)
                .HasForeignKey(d => d.IplfranchiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_IPLFranchise");
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity.ToTable("ProductSize");

            entity.Property(e => e.ProductSizeId).HasColumnName("ProductSizeID");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.SizeName).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductSizeAvailability>(entity =>
        {
            entity.HasKey(e => e.ProductSizeId);

            entity.ToTable("ProductSizeAvailability");

            entity.Property(e => e.ProductSizeId)
                .ValueGeneratedNever()
                .HasColumnName("ProductSizeID");
            entity.Property(e => e.CreatedOn).HasColumnName("CreatedON");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasDefaultValue(0);
            entity.Property(e => e.SizeId).HasColumnName("SizeID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSizeAvailabilities)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductSizeAvailability_Product");

            entity.HasOne(d => d.ProductSize).WithOne(p => p.ProductSizeAvailability)
                .HasForeignKey<ProductSizeAvailability>(d => d.ProductSizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductSizeAvailability_ProductSize");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.ToTable("Tenant");

            entity.Property(e => e.TenantId)
                .ValueGeneratedNever()
                .HasColumnName("TenantID");
            entity.Property(e => e.TeanatName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
