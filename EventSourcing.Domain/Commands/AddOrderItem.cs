using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Commands;
using EventFlow.Core;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders.Commands
{
    public class AddOrderItem: Command<OrderAggregate, OrderId>
    {
        public OrderItem OrderItem { get; }

        public AddOrderItem(OrderId aggregateId, OrderItem orderItem) : base(aggregateId)
        {
            OrderItem = orderItem;
        }
    }
}
