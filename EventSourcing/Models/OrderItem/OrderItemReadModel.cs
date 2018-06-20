using System;

namespace EventSourcing.Models.OrderItem
{
    public class OrderItemReadModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}