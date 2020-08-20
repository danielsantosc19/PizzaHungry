using System;
using System.Collections.Generic;
using System.Text;

namespace Logstore_BackEnd.Model
{
    public interface IOrderItem
    {   
        int Quantity { get; set; }
        decimal Price { get; }

    }
}
