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
    [Table("OrderLines")]
    public class OrderLineReadModel : IReadModel,
        IAmReadModelFor<OrderAggregate, OrderId, OrderItemAdded>
    {
        [MsSqlReadModelIdentityColumn]
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }

        [MsSqlReadModelVersionColumn]
        public int Version { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<OrderAggregate, OrderId, OrderItemAdded> domainEvent)
        {
            OrderId = domainEvent.AggregateIdentity.Value;
            Id = domainEvent.AggregateEvent.OrderItem.Id.Value;
            ProductId = domainEvent.AggregateEvent.OrderItem.ProductId.Value;
            Title = domainEvent.AggregateEvent.OrderItem.Title;
            Price = domainEvent.AggregateEvent.OrderItem.Price;
            Amount = domainEvent.AggregateEvent.OrderItem.Amount;
        }
    }
}
