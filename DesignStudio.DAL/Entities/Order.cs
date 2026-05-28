namespace DesignStudio.DAL.Entities
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public enum OrderType
    {
        ServiceBased,   // з переліку послуг
        Turnkey         // дизайн «під ключ»
    }

    public class Order
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string Requirements { get; set; }
        public OrderStatus Status { get; set; }
        public OrderType Type { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
        public PortfolioItem PortfolioItem { get; set; }
    }
}