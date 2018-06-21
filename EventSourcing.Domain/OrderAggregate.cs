using EventFlow.Aggregates;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.Events;
using EventSourcing.Domain.Orders.State;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders
{
    public class OrderAggregate : AggregateRoot<OrderAggregate, OrderId>,
        IApply<OrderCreated>
    {
        private readonly OrderState _orderState = new OrderState();

        public Username User { get; set; }

        public OrderAggregate(OrderId id) : base(id)
        {
            Register(_orderState);
        }

        public void Create(Username user)
        {
            //Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
            Emit(new OrderCreated(user));
        }

        public void AddOrderItem(OrderLine orderItem)
        {
            Emit(new OrderLineAdded(orderItem));
        }

        public void Apply(OrderCreated aggregateEvent)
        {
            User = aggregateEvent.User;
        }
    }
}
