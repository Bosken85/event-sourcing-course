using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace EventSourcing.Domain.Orders
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class OrderId : Identity<OrderId>
    {
        public OrderId(string value) : base(value)
        {
        }
    }
}