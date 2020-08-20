using Logstore_BackEnd.Model;
using Logstore_BackEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;


namespace Logstore_BackEnd.Extensions
{
    public static class Extensions
    {
        public static FlavorDto MapFlavorToDto(this Flavor flavor)
        {
            if (flavor == null) return null;

            var flavorDto = new FlavorDto()
            {
                Id = flavor.Id,
                Name = flavor.Name,
                Price = flavor.Price
            };

            return flavorDto;
        }
        public static OrderHistoryDto MapOrderToHistoryDto(this Order order)
        {
            var orderHistory = new OrderHistoryDto()
            {
                Id = order.Id,
                //Client = order.Client,
                FinalPrice = order.FinalPrice,
                Items = order.Items.Select(i => new OrderItemHistoryDto()
                {
                    Flavor1 = i.Flavor1.MapFlavorToDto(),
                    Flavor2 = i.Flavor2.MapFlavorToDto(),
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            return orderHistory;

        }
    }
}
