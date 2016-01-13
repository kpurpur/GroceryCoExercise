using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryCo.Data;
using GroceryCo.DTO;

namespace GroceryCo.Service
{
    public interface IProductService
    {
        List<ProductDTO> GetProducts();
    }
}
