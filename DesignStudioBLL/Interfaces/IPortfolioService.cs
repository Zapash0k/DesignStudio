using DesignStudio.BLL.Models;

namespace DesignStudio.BLL.Interfaces
{
    public interface IPortfolioService
    {
        Task<IEnumerable<PortfolioItemModel>> GetAllAsync();
        Task<IEnumerable<PortfolioItemModel>> GetByCategoryAsync(string category);
        Task AddPortfolioItemAsync(string title, string description,
                                  string category, string imageUrl, int? orderId);
    }
}