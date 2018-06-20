using EventFlow.Aggregates;
using EventFlow.EventStores;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders.Events
{
    [EventVersion("OrderItemAdded", 1)]
    public class OrderItemAdded: AggregateEvent<OrderAggregate, OrderId>
    {
        public OrderItem OrderItem { get; }

        public OrderItemAdded(OrderItem orderItem)
        {
            OrderItem = orderItem;
        }
    }
}
