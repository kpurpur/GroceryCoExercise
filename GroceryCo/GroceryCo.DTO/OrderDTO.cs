﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCo.DTO
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
