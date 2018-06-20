using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using EventSourcing.Models.Orders;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Controllers
{
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _processor;

        public OrdersController(ICommandBus commandBus, IQueryProcessor query)
        {
            _commandBus = commandBus;
            _processor = query;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(null);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(null);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string value)
        {
            return CreatedAtAction("Get", new {id = Guid.NewGuid()}, null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
