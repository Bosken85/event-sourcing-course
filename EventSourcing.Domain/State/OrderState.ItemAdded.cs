using System.Collections.Generic;
using System.Linq;
using EventFlow.Aggregates;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.Events;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders.State
{
    public partial class OrderState : IApply<OrderLineAdded>
    {
        public List<OrderLine> OrderItems { get; } = new List<OrderLine>();

        public void Apply(OrderLineAdded aggregateEvent)
        {
            var orderLine = aggregateEvent.OrderLine;
            var existing = OrderItems.FirstOrDefault(x => x.ProductId == orderLine.ProductId);
            if (existing != null)
            {
                existing.Amount += orderLine.Amount;
            }
            else
            {
                OrderItems.Add(orderLine);
            }
        }
    }
}
