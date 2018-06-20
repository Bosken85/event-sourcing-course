using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace EventSourcing.Domain.Products
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class ProductId : Identity<ProductId>
    {
        public ProductId(string value) : base(value)
        {
        }
    }
}
