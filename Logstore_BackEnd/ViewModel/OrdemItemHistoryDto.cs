using System;
using System.Collections.Generic;
using System.Text;

namespace Logstore_BackEnd.ViewModel
{
    public class OrderItemHistoryDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public FlavorDto Flavor1 { get; set; }
        public FlavorDto Flavor2 { get; set; }
        public decimal Price { get; set; }

    }
}
