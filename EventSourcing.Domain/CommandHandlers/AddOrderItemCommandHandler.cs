using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventSourcing.Domain.Orders.Commands;

namespace EventSourcing.Domain.Orders.CommandHandlers
{
    public class AddOrderItemCommandHandler: CommandHandler<OrderAggregate, OrderId, AddOrderLine>
    {
        public override Task ExecuteAsync(OrderAggregate aggregate, AddOrderLine command, CancellationToken cancellationToken)
        {
            aggregate.AddOrderItem(command.OrderLine);
            return Task.FromResult(0);
        }
    }
}
