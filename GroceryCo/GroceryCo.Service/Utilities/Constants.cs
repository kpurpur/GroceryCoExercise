using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCo.Service.Utilities
{
    public class Constants
    {
        public class OrderStatus
        {
            public const string Pending = "pend";
            public const string Complete = "comp";
        }

        public class PromotionType
        {
            public const string Discount = "d";
            public const string Price = "p";
        }
    }
}
