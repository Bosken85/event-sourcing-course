using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventSourcing.Domain.Orders.Commands;

namespace EventSourcing.Domain.Orders.CommandHandlers
{
    public class AddOrderItemCommandHandler: CommandHandler<OrderAggregate, OrderId, AddOrderItem>
    {
        public override Task ExecuteAsync(OrderAggregate aggregate, AddOrderItem command, CancellationToken cancellationToken)
        {
            aggregate.AddOrderItem(command.OrderItem);
            return Task.FromResult(0);
        }
    }
}
