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
using EventSourcing.Models.OrderItem;
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
        public async Task<IActionResult> Get([FromRoute]string orderId)
        {
            var result = await _processor.ProcessAsync(new GetOrderLinesByOrderIdQuery(orderId), new CancellationToken());
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]string orderId, [FromRoute]string id)
        {
            var orderLine = await _processor.ProcessAsync(new ReadModelByIdQuery<OrderLineReadModel>(id), new CancellationToken());
            return Ok(orderLine);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromRoute]string orderId, [FromBody]OrderItemWriteModel value)
        {
            var orderIdKey = OrderId.With(orderId);
            var orderItemId = OrderItemId.NewComb();
            var orderItem = new OrderItem(orderItemId, ProductId.NewComb(), value.Title, value.Price, value.Amount);
            var cmd = new AddOrderItem(orderIdKey, orderItem);
            await this._commandBus.PublishAsync(cmd, new CancellationToken()).ConfigureAwait(false);
            return CreatedAtAction("Get", new { orderId = orderIdKey.Value, id = orderItemId.Value }, null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
