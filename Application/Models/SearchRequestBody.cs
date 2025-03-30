using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SearchRequestBody
    {
        //Product SearchObject properties
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string? CategoryName { get; set; }
        public string? FranchiseName { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }

    public class CreateCartRequestBody
    {
        //Add items to cart properties
        public long CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; }

    }

    public class CreateOrderRequestBody
    {
        //Add items to cart properties
        public long CustomerId { get; set; }
        public long CartId { get; set; }

    }
}
