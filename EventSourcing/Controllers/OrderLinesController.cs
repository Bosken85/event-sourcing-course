using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using EventSourcing.Models;
using EventSourcing.Models.OrderLines;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Controllers
{
    [Route("api/orders/{orderId}/lines")]
    public class OrderLinesController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _processor;

        public OrderLinesController(ICommandBus commandBus, IQueryProcessor processor)
        {
            _commandBus = commandBus;
            _processor = processor;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute]Guid orderId)
        {
            return Ok(null);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid orderId, [FromRoute]Guid id)
        {
            return Ok(null);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromRoute]Guid orderId, [FromBody]OrderLineWrite value)
        {
            return CreatedAtAction("Get", new { orderId = Guid.NewGuid(), id = Guid.NewGuid() }, null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
