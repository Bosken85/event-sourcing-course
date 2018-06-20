using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace EventSourcing.Domain.Orders.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class OrderLineId : Identity<OrderLineId>
    {
        public OrderLineId(string value) : base(value)
        {
        }
    }
}