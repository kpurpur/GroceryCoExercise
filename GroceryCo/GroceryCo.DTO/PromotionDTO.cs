using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCo.DTO
{
    public class PromotionDTO
    {
        public long Id { get; set; }
        public string ProductCode { get; set; }
        public string PromotionCode { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> ApplyTo { get; set; }
        public decimal Amount { get; set; }
    }
}
