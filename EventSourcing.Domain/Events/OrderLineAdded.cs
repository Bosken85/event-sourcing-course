using EventFlow.Aggregates;
using EventFlow.EventStores;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders.Events
{
    [EventVersion("OrderLineAdded", 1)]
    public class OrderLineAdded : AggregateEvent<OrderAggregate, OrderId>
    {
        public OrderLine OrderLine { get; }

        public OrderLineAdded(OrderLine orderLine)
        {
            OrderLine = orderLine;
        }
    }
}
