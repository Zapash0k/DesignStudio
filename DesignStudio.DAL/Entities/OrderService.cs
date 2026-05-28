namespace DesignStudio.DAL.Entities
{
    public class OrderService
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}