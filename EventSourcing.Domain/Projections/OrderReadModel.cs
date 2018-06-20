using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores.Attributes;
using EventFlow.ReadStores;
using EventSourcing.Domain.Orders.Events;

namespace EventSourcing.Domain.Orders.Projections
{
    [Table("Orders")]
    public class OrderReadModel : IReadModel,
        IAmReadModelFor<OrderAggregate, OrderId, OrderCreated>
    {
        [MsSqlReadModelIdentityColumn]
        public string Id { get; set; }

        public string Username { get; set; }

        [MsSqlReadModelVersionColumn]
        public int Version { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<OrderAggregate, OrderId, OrderCreated> domainEvent)
        {
            this.Id = domainEvent.AggregateIdentity.Value;
            this.Username = domainEvent.AggregateEvent.User.Value;
        }
    }
}
