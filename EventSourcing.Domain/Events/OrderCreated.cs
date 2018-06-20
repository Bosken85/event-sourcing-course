using EventFlow.Aggregates;
using EventFlow.EventStores;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders.Events
{
    [EventVersion("OrderCreated", 1)]
    public class OrderCreated: AggregateEvent<OrderAggregate, OrderId>
    {
        public Username User { get; set; }

        public OrderCreated(Username user)
        {
            User = user;
        }
    }
}
