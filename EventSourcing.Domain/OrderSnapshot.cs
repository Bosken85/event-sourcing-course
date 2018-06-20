using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Snapshots;
using EventSourcing.Domain.Orders.Entities;
using EventSourcing.Domain.Orders.ValueObjects;

namespace EventSourcing.Domain.Orders
{
    [SnapshotVersion("Order", 1)]
    public class OrderSnapshot: ISnapshot
    {
        public Username User { get; set; }

        public List<OrderLine> OrderItems { get; set; } = new List<OrderLine>();
    }
}
