using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using EventSourcing.Domain.Orders.Commands;

namespace EventSourcing.Domain.Orders.CommandHandlers
{
    public class CreateOrderCommandHandler : CommandHandler<OrderAggregate, OrderId, CreateOrder>
    {
        public override Task ExecuteAsync(OrderAggregate aggregate, CreateOrder command, CancellationToken cancellationToken)
        {
            aggregate.Create(command.User);
            return Task.FromResult(0);
        }
    }
}
