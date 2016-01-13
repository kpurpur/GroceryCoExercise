using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCo.DTO
{
    public class ProductDTO
    {
        public string Code { get; set; }
        public string ProductTypeCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<PromotionDTO> Promotions { get; set; }
    }
}
