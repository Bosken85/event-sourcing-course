namespace EventSourcing.Models.OrderItems
{
    public class OrderItemWrite
    {
        public string Title { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}
