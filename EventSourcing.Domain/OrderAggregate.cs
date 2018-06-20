using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.Events;
using EventSourcing.Domain.Orders.State;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders
{
    public class OrderAggregate : SnapshotAggregateRoot<OrderAggregate, OrderId, OrderSnapshot>,
        IApply<OrderCreated>
    {
        private readonly OrderSate _orderState = new OrderSate();

        public Username User { get; set; }

        public OrderAggregate(OrderId id) : base(id, SnapshotEveryFewVersionsStrategy.With(5))
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

        protected override async Task<OrderSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            var snapshot = new OrderSnapshot
            {
                User = this.User,
                OrderItems = this._orderState.OrderItems
            };
            return await Task.FromResult(snapshot).ConfigureAwait(false);
        }

        protected override Task LoadSnapshotAsync(OrderSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
        {
            this.User = snapshot.User;
            this._orderState.OrderItems.AddRange(snapshot.OrderItems);
            return Task.CompletedTask;
        }
    }
}
