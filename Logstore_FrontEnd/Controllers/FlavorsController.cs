using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Logstore_BackEnd;
using Logstore_BackEnd.Model;
using Logstore_BackEnd.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logstore_Daniel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FlavorsController : ControllerBase
    {
        private readonly IFlavorRepository _flavorRepository;
        private readonly HungryPizzaDbContext _dbContext;

        public FlavorsController(IFlavorRepository flavorRepository,HungryPizzaDbContext dbContext)
        {
            _flavorRepository = flavorRepository;
            _dbContext = dbContext;
        }

        // GET api/flavors
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<IList<Flavor>>> Get()
        {
            var result = await _flavorRepository.Get();
            
            return Ok(result);
        }

        // GET api/flavors/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<ActionResult<IList<Flavor>>> Get(int id)
        {
            var result = await _flavorRepository.GetById(id);

            if (result == null)
                return BadRequest("Not found");
            
            return Ok(result);
        }

        // POST api/flavors
        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<ActionResult<Flavor>> Post([FromBody]Flavor flavor)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _flavorRepository.Add(flavor);
            return CreatedAtAction("Get", new { id = flavor.Id }, flavor);
        }
    }
}
