namespace DesignStudio.BLL.Models
{
    public enum OrderStatusModel { Pending, InProgress, Completed, Cancelled }
    public enum OrderTypeModel { ServiceBased, Turnkey }

    public class OrderModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string Requirements { get; set; }
        public OrderStatusModel Status { get; set; }
        public OrderTypeModel Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ServiceModel> Services { get; set; } = new();
    }
}