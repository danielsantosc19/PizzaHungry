using Logstore_BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logstore_BackEnd.ViewModel
{
    public class OrderHistoryDto
    {
        public int Id { get; set; }        
        public decimal FinalPrice { get; set; }
        public List<OrderItemHistoryDto> Items { get; set; } = new List<OrderItemHistoryDto>();
        
    }
}
