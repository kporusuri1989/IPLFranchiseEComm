using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Tenant
{
    [Key]
    public int TenantId { get; set; }

    public string TeanatName { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<Iplfranchise> Iplfranchises { get; set; } = new List<Iplfranchise>();
}
