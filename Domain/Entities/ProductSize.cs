using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductSize
    {
        [Key]
        public int ProductSizeId { get; set; }

        public string? SizeName { get; set; }

        public string? Description { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ProductSizeAvailability? ProductSizeAvailability { get; set; }
    }
}
