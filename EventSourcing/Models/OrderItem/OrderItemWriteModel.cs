namespace EventSourcing.Models.OrderItem
{
    public class OrderItemWriteModel
    {
        public string Title { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}
