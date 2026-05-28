namespace DesignStudio.DAL.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } // "Web", "Interior", "Branding", etc.

        public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
    }
}