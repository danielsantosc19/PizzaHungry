using System;
using System.Collections.Generic;
using System.Text;

namespace Logstore_BackEnd.ViewModel
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public decimal Price { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

        public OrderDto()
        {
        }
    }
}
