using System;
using System.Collections.Generic;
using System.Text;
using EventFlow.Exceptions;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace EventSourcing.Domain.Orders.ValueObjects
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class Username : SingleValueObject<string>
    {
        public Username(string value) : base(value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 100)
            {
                throw DomainError.With($"Invalid username '{value}'");
            }
        }
    }
}
