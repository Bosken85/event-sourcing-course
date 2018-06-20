using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Queries;
using EventSourcing.Domain.Orders.Projections;

namespace EventSourcing.Domain.Orders.Queries
{
    public class GetOrderLinesByOrderIdQuery : IQuery<IReadOnlyCollection<OrderLineReadModel>>
    {
        public string OrderId { get; set; }

        public GetOrderLinesByOrderIdQuery(string orderId)
        {
            OrderId = orderId;
        }
    }
}
