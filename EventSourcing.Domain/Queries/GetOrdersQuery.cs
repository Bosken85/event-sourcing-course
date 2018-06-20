using System.Collections.Generic;
using EventFlow.Queries;
using EventSourcing.Domain.Orders.Projections;

namespace EventSourcing.Domain.Orders.Queries
{
    public class GetOrdersQuery : IQuery<IReadOnlyCollection<OrderReadModel>>
    {
    }
}