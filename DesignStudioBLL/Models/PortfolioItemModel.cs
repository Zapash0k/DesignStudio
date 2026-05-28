namespace DesignStudio.BLL.Models
{
    public class PortfolioItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime CompletedAt { get; set; }
        public string ImageUrl { get; set; }
        public int? OrderId { get; set; }
    }
}