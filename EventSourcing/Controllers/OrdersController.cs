using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using EventSourcing.Domain.Orders;
using EventSourcing.Domain.Orders.Commands;
using EventSourcing.Domain.Orders.Projections;
using EventSourcing.Domain.Orders.Queries;
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
            
            var result = await _processor.ProcessAsync(new GetOrdersQuery(), new CancellationToken());
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<OrderReadModel> Get(string id)
        {
            var orderId = OrderId.With(id);
            return await _processor.ProcessAsync(new ReadModelByIdQuery<OrderReadModel>(orderId), new CancellationToken());
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string value)
        {
            var orderId = OrderId.NewComb();
            await this._commandBus.PublishAsync(new CreateOrder(orderId, value), new CancellationToken()).ConfigureAwait(false);
            return CreatedAtAction("Get", new {id = orderId.Value}, null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
