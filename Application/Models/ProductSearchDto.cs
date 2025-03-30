using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductSearchDto
    {
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? FranchiseName { get; set; }
    public string? Category { get; set; }
    public decimal? minPrice { get; set;  }
    public decimal? maxPrice { get; set; }
    }
}
