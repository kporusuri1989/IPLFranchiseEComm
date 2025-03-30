using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class CustomerCart
{
    [Key]
    public long CartId { get; set; }

    public long CustomerId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Customer Customer { get; set; } = null!;

}
