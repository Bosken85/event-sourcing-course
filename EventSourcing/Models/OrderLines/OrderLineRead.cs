﻿using System;

namespace EventSourcing.Models.OrderLines
{
    public class OrderLineRead
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}