using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Logstore_BackEnd.Model
{
    public class Order : BaseEntity
    {
        
        public decimal Freight { get; set; } = 0;
        public string Address { get; set; }
        public virtual ICollection<Pizza> Items { get; set; }

        public virtual Client Client { get; set; }
        public int? ClientId { get; set; }

        public Order()
        {
            Items = new List<Pizza>();
        }
        public decimal FinalPrice
        {
            get
            {
                return Items.Sum(x => x.Price) + Freight;
            }
        }

    }
}
