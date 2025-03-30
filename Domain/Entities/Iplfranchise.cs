using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Iplfranchise
{
    [Key]
    public int IplfranchiseId { get; set; }

    public int TenantId { get; set; }

    public string IplteamName { get; set; } = null!;

    public string? Description { get; set; }

    public string? IsActive { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Tenant Tenant { get; set; } = null!;
}
