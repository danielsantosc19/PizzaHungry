using System;
using System.Collections.Generic;
using System.Text;

namespace Logstore_BackEnd.Model
{
    public class Flavor : BaseEntity
    {
        
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
