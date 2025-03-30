using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Types
{
    public static class EnumTypes
    {
        public enum OrderStatusTypes
        {
            Confirmed=1,
            Cancelled=2,
            Refunded=3,
            Declined=4
        }
        public enum PaymentMethods
        {
            COD = 1,
            CARD = 2
        }
    }
}
