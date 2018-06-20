using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core;
using EventFlow.MsSql;
using EventFlow.Queries;
using EventSourcing.Domain.Orders.Projections;
using EventSourcing.Domain.Orders.Queries;

namespace EventSourcing.Domain.Orders.QueryHandlers
{
    public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, IReadOnlyCollection<OrderReadModel>>
    {
        private readonly IMsSqlConnection _msSqlConnection;

        public GetOrdersQueryHandler(IMsSqlConnection connection)
        {
            _msSqlConnection = connection;
        }

        public async Task<IReadOnlyCollection<OrderReadModel>> ExecuteQueryAsync(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var readModels = await _msSqlConnection.QueryAsync<OrderReadModel>(
                    Label.Named("Orders"),
                    cancellationToken,
                    "SELECT * FROM [Orders]")
                .ConfigureAwait(false);

            return readModels.ToList();
        }
    }
}