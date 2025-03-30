using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class CustomerCartDto
    {
        public long CartId { get; set; }

        public long CustomerId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual List<CartItem> CartItems { get; set; } = null!;
    }
}
