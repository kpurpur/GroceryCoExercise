using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCo.DTO
{
    public class ShoppingCartDTO
    {
        public ICollection<ShoppingCartItemDTO> ShoppingCartItems { get; set; }
    }
}
