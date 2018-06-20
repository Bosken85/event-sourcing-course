using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcing.Models.Orders
{
    public class OrderRead
    {
        public Guid Id { get; set; }

        public String Username { get; set; }

        public int ItemCount { get; set; }

        public double PriceTotal { get; set; }
    }
}
