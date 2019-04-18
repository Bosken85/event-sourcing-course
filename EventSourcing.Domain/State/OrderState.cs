using EventFlow.Aggregates;

namespace EventSourcing.Domain.Orders.State
{
    public partial class OrderState: AggregateState<OrderAggregate, OrderId, OrderState>
    {
    }
}
