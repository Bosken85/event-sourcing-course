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
            
            var domainResult = await _processor.ProcessAsync(new GetOrdersQuery(), new CancellationToken());
            var result = domainResult.Select(x => new OrderRead
            {
                Id = OrderId.With(x.Id).GetGuid(),
                Username = x.Username,
                ItemCount =  x.ItemCount,
                PriceTotal = x.PriceTotal
            }).ToList();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var orderId = OrderId.With(id);
            var domainResult = await _processor.ProcessAsync(new ReadModelByIdQuery<OrderReadModel>(orderId), new CancellationToken());

            if (domainResult == null) return NotFound();

            var result = new OrderRead
            {
                Id = OrderId.With(domainResult.Id).GetGuid(),
                Username = domainResult.Username,
                ItemCount = domainResult.ItemCount,
                PriceTotal = domainResult.PriceTotal
            };
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string value)
        {
            var orderId = OrderId.NewComb();
            await _commandBus.PublishAsync(new CreateOrder(orderId, value), new CancellationToken());
            return CreatedAtAction("Get", new {id = orderId.GetGuid()}, null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var orderId = OrderId.With(id);
        }
    }
}
