namespace EventSourcing.Models.OrderLines
{
    public class OrderLineWrite
    {
        public string Title { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}
