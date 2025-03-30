using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CreateCartDto
    {
        public long CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; }
    }

    public class CartItemDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public int ProductSizeID { get; set; }
    }
}
