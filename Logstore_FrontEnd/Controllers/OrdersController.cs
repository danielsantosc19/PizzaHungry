using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Logstore_BackEnd;
using Logstore_BackEnd.Extensions;
using Logstore_BackEnd.Model;
using Logstore_BackEnd.Repository;
using Logstore_BackEnd.Services;
using Logstore_BackEnd.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logstore_Daniel.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly HungryPizzaDbContext _dbContext;
        private readonly OrderServices _orderServices;

        public OrdersController(IOrderRepository orderRepository,HungryPizzaDbContext dbContext,OrderServices orderServices)
        {
            _orderRepository = orderRepository;
            _dbContext = dbContext;
            _orderServices = orderServices;

        }

        // GET api/orders
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<IList<Order>>> Get([FromQuery] FilterOrder filterOrder)
        {
            var result = await _orderServices.GetOrderHistory(filterOrder);
            return Ok(result);
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var result = await _orderRepository.GetById(id);

            if (result == null)
                return BadRequest("Not found");
            
            return Ok(result);
        }        

        // POST api/orders
        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST api/order
        public async Task<ActionResult<Order>> Post([FromBody]OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany (x => x.Errors ));
            Order order;

            try
            {
                order = await _orderServices.ProcessNewOrder(orderDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return CreatedAtAction("Get", new { id = order.Id }, order.MapOrderToHistoryDto());
        }
       
    }
}
