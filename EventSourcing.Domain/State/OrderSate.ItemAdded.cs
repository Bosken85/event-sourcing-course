using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.Events;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders.State
{
    public partial class OrderSate : IApply<OrderItemAdded>
    {
        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public void Apply(OrderItemAdded aggregateEvent)
        {
            var orderItem = aggregateEvent.OrderItem;
            var existing = OrderItems.FirstOrDefault(x => x.ProductId == orderItem.ProductId);
            if (existing != null)
            {
                existing.Amount += orderItem.Amount;
            }
            else
            {
                OrderItems.Add(orderItem);
            }
        }
    }
}
