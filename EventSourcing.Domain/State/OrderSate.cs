using EventFlow.Aggregates;

namespace EventSourcing.Domain.Orders.State
{
    public partial class OrderSate: AggregateState<OrderAggregate, OrderId, OrderSate>
    {
    }
}
