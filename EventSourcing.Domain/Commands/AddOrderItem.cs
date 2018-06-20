using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Commands;
using EventFlow.Core;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders.Commands
{
    public class AddOrderLine: Command<OrderAggregate, OrderId>
    {
        public OrderLine OrderLine { get; }

        public AddOrderLine(OrderId aggregateId, OrderLine orderLine) : base(aggregateId)
        {
            OrderLine = orderLine;
        }
    }
}
