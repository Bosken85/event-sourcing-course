using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace EventSourcing.Domain.Orders.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class OrderItemId : Identity<OrderItemId>
    {
        public OrderItemId(string value) : base(value)
        {
        }
    }
}