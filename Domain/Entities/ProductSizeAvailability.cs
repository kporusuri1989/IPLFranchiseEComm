using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public partial class ProductSizeAvailability
    {
        [Key]
        public int ProductSizeId { get; set; }

        public int SizeId { get; set; }

        public long ProductId { get; set; }

        public int? Quantity { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual ProductSize ProductSize { get; set; } = null!;
    }

}
