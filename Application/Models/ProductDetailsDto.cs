using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDetailsDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string CategoryName { get; set; }
        public string IPLFranchiseName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
