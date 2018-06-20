using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.Queries;
using EventSourcing.Domain.Orders.Projections;
using EventSourcing.Domain.Orders.Queries;

namespace EventSourcing.Domain.Orders.QueryHandlers
{
    public class GetOrderLinesByOrderIdQueryHandler: IQueryHandler<GetOrderLinesByOrderIdQuery, IReadOnlyCollection<OrderLineReadModel>>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetOrderLinesByOrderIdQueryHandler(IMsSqlConnection connection)
        {
            _msSqlConnection = connection;
        }

        public async Task<IReadOnlyCollection<OrderLineReadModel>> ExecuteQueryAsync(GetOrderLinesByOrderIdQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<OrderLineReadModel>(
                    Label.Named("OrderLines"),
                    cancellationToken,
                    "SELECT * FROM [OrderLines] WHERE OrderId = @OrderId",
                    new { OrderId = query.OrderId})
                .ConfigureAwait(false);

            return readModels.ToList();
        }
    }
}
