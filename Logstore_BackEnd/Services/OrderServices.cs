using Logstore_BackEnd.Extensions;
using Logstore_BackEnd.Model;
using Logstore_BackEnd.Repository;
using Logstore_BackEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logstore_BackEnd.Services
{
    public class OrderServices
    {
        private readonly IFlavorRepository _flavorRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPizzaRepository _pizzaRepository;

        public OrderServices(IOrderRepository orderRepository, IFlavorRepository flavorRepository, IClientRepository clientRepository,IPizzaRepository pizzaRepository)
        {
            _orderRepository = orderRepository;
            _flavorRepository = flavorRepository;
            _clientRepository = clientRepository;
            _pizzaRepository = pizzaRepository;
        }

        public async Task<Order> ProcessNewOrder(OrderDto orderDto)
        {
            if (orderDto.Items.Count == 0 || orderDto.Items.Count > 10)
                throw new Exception("O pedido deve ter no mínimo uma pizza e no máximo 10.");
            
            if (string.IsNullOrEmpty(orderDto.Address) && !orderDto.ClientId.HasValue)
                throw new Exception("O pedido precisa da identificação do cliente cadastrado ou um endereço para entrega");

            if (orderDto.ClientId.HasValue)
            {
                var client = await _clientRepository.GetById(orderDto.ClientId.Value);
                if (client == null)
                    throw new Exception("Cliente inválido");
            }
            else
            {
                var client = new Client()
                {
                    Address = orderDto.Address,
                    Name = string.IsNullOrEmpty(orderDto.Name) ? "Cliente " + DateTime.Now.ToString() : orderDto.Name
                };

                await _clientRepository.Add(client);
                orderDto.ClientId = client.Id;
            }
            
            var order = new Order()
            {
                Address = orderDto.Address,
                ClientId = orderDto.ClientId,
                Freight = 0                
            };

            foreach(OrderItemDto orderItemDto in orderDto.Items)
            {
                var orderItem = new Pizza()
                {
                    Flavor1Id = orderItemDto.Flavor,
                    Flavor2Id = orderItemDto.Flavor2,                    
                    Quantity = 1

                };
                
                if (await _flavorRepository.GetById(orderItem.Flavor1Id) == null)                
                    throw new Exception("Sabor da pizza inválido");

                if (orderItem.Flavor2Id.HasValue)
                {
                    if (await _flavorRepository.GetById(orderItem.Flavor2Id.Value) == null)
                        throw new Exception("Segundo sabor da pizza inválido");
                }

                order.Items.Add(orderItem);

            }
            
            await _orderRepository.Add(order);

            return await _orderRepository.GetById(order.Id);


        }

        public async Task<IList<OrderHistoryDto>> GetOrderHistory(FilterOrder filterOrder)
        {
            IList<Order> orders;
            if (filterOrder.ClientId.HasValue)
                orders = await _orderRepository.Get(o => o.ClientId == filterOrder.ClientId.Value);
            else
                orders = await _orderRepository.Get();

            var ordersHistory = new List<OrderHistoryDto>();

            foreach (Order order in orders)
                ordersHistory.Add(order.MapOrderToHistoryDto());

            return ordersHistory;

        }
    }
}
