using EventFlow.Commands;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders.Commands
{
    public class CreateOrder : Command<OrderAggregate, OrderId>
    {
        public Username User { get; set; }

        public CreateOrder(OrderId id, string user) : base(id)
        {
            User = new Username(user);
        }
    }
}
