using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Product
{
    [Key]
    public long ProductId { get; set; }

    public int CategoryId { get; set; }

    public int TenantId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int IplfranchiseId { get; set; }

    public DateTime? CraetedOn { get; set; }

    public bool IsActive { get; set; }

    public string? Description { get; set; }

    public string? ProductCode { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category Categories { get; set; } = null!;

    public virtual Iplfranchise Iplfranchise { get; set; } = null!;

    public virtual ICollection<OrderDetailItem> OrderDetailItems { get; set; } = new List<OrderDetailItem>();

    //public virtual ICollection<OrderDetail> Orders { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductSizeAvailability> ProductSizeAvailabilities { get; set; } = new List<ProductSizeAvailability>();
}

