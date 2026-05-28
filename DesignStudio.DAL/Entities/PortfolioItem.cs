namespace DesignStudio.DAL.Entities
{
    public class PortfolioItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime CompletedAt { get; set; }
        public string ImageUrl { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}