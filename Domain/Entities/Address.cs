using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Address
{
    [Key]
    public int AddressId { get; set; }

    public string FullName { get; set; } = null!;

    public string AddLine1 { get; set; } = null!;

    public string? AddLine2 { get; set; }

    public long? ZipCode { get; set; }

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public bool IsActive { get; set; }

    public long CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
