using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Logstore_BackEnd;
using Logstore_BackEnd.Extensions;
using Logstore_BackEnd.Model;
using Logstore_BackEnd.Repository;
using Logstore_BackEnd.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logstore_Daniel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly HungryPizzaDbContext _dbContext;

        public ClientsController(IClientRepository clientRepository,HungryPizzaDbContext dbContext,IOrderRepository orderRepository)
        {
            _clientRepository = clientRepository;
            _dbContext = dbContext;
            _orderRepository = orderRepository;
        }

        // GET api/clients
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<IList<Client>>> Get()
        {
            var result = await _clientRepository.Get();
            
            return Ok(result);
        }

        // GET api/clients/5
        [HttpGet("{id}")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<Client>>> Get(int id)
        {
            var result = await _clientRepository.GetById(id);

            if (result == null)
                return BadRequest("Not found");
            
            return Ok(result);
        }

        // GET api/clients/5/orders
        [Route("{id}/orders")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<OrderHistoryDto>>> GetOrders(int id)
        {
            var result = await _orderRepository.Get(o => o.ClientId == id);
            if (result == null)
                return BadRequest("Not found");

            var orders = new List<OrderHistoryDto>();

            foreach (Order order in result)            
                orders.Add(order.MapOrderToHistoryDto());            

            return Ok(orders);
        }

        // POST api/clients
        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        // POST api/clients
        public async Task<ActionResult<Client>> Post([FromBody]Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany (x => x.Errors ));

            await _clientRepository.Add(client);
            return CreatedAtAction("Get", new { id = client.Id }, client);
        }        
    }
}
