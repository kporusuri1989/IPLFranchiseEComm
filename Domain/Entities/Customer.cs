using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Customer
{
    [Key]
    public long CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public bool IsActive { get; set; }

    public string PhoneNum { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<CustomerCart> CustomerCarts { get; set; } = new List<CustomerCart>();

    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
}
