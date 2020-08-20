using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Logstore_BackEnd.Model
{
    public class Pizza : BaseEntity, IOrderItem
    {   
        public int Quantity { get; set; } = 1;
        public int Flavor1Id { get; set; }
        public int? Flavor2Id { get; set; }

        [ForeignKey("Flavor1Id")]
        public Flavor Flavor1 { get; set; }

        [ForeignKey("Flavor2Id")]
        public Flavor Flavor2 { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        
        public int OrderId { get; set; }


        public decimal Price 
        {
            get
            {
                if (Flavor1 == null) return 0;

                if (Flavor2 == null) return Flavor1.Price * Quantity;

                return ((Flavor1.Price + Flavor2.Price) / 2)* Quantity;
            } 
        }


    }
}
