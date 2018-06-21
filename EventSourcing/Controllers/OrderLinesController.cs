using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using EventSourcing.Domain.Orders;
using EventSourcing.Domain.Orders.Commands;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.Projections;
using EventSourcing.Domain.Orders.Queries;
using EventSourcing.Domain.Products;
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
            var orderIdValue = OrderId.With(orderId).Value;
            var domainResults = await _processor.ProcessAsync(new GetOrderLinesByOrderIdQuery(orderIdValue), new CancellationToken());
            var results = domainResults.Select(x => new OrderLineRead
            {
                Id = OrderLineId.With(x.Id).GetGuid(),
                ProductId = ProductId.With(x.ProductId).GetGuid(),
                Title = x.Title,
                Price = x.Price,
                Amount = x.Amount
            }).ToList();
            return Ok(results);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid orderId, [FromRoute]Guid id)
        {
            var orderLineIdValue = OrderLineId.With(id).Value;
            var orderLine = await _processor.ProcessAsync(new ReadModelByIdQuery<OrderLineReadModel>(orderLineIdValue), new CancellationToken());
            return Ok(orderLine);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromRoute]Guid orderId, [FromBody]OrderLineWrite value)
        {
            var orderIdKey = OrderId.With(orderId);
            var orderLineId = OrderLineId.NewComb();
            var orderLine = new OrderLine(orderLineId, ProductId.NewComb(), value.Title, value.Price, value.Amount);
            var cmd = new AddOrderLine(orderIdKey, orderLine);
            await this._commandBus.PublishAsync(cmd, new CancellationToken()).ConfigureAwait(false);
            return CreatedAtAction("Get", new { orderId = orderIdKey.GetGuid(), id = orderLineId.GetGuid() }, null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
