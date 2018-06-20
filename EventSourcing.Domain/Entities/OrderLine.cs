using EventFlow.Entities;
using EventFlow.ValueObjects;
using EventSourcing.Domain.Products;

namespace EventSourcing.Domain.Orders.Entities
{
    public class OrderLine : Entity<OrderLineId>
    {
        public ProductId ProductId { get; }

        public string Title { get; }

        public double Price { get; }

        public int Amount { get; internal set; }

        public OrderLine(OrderLineId id, ProductId productId, string title, double price, int amount) : base(id)
        {
            ProductId = productId;
            Title = title;
            Price = price;
            Amount = amount;
        }
    }
}
