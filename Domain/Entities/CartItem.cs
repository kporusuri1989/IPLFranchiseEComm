using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class CartItem
{
    [Key]
    public long CartItemId { get; set; }

    public long ProductId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public long? CartId { get; set; }

    public int Quantity { get; set; }

    public int? ProductSizeId { get; set; }

    public virtual CustomerCart? Cart { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductSize? ProductSize { get; set; }
}
