using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{

    public partial class OrderDto
    {
        public long OrderId { get; set; }

        public long CustomerId { get; set; }

        //public long ProductId { get; set; }

        public DateTime OrderDate { get; set; }

        public int? PaymentMethodId { get; set; }

        public string? CardNo { get; set; }

        public int OrderStatusId { get; set; }

        public decimal? ProductPrice { get; set; }

        public virtual Customer Customer { get; set; } = null!;

        public virtual ICollection<OrderDetailItemDto> OrderItems { get; set; } = new List<OrderDetailItemDto>();

        public virtual OrderStatus OrderStatus { get; set; } = null!;

        public virtual PaymentMethod? PaymentMethod { get; set; }

    }
}
