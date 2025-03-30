using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetailItem
    {
        [Key]
        public long OrderItemId { get; set; }

        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        //public virtual OrderDetail OrderDetail { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
