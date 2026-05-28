using DesignStudio.DAL.Entities;

namespace DesignStudio.DAL.Interfaces
{
    public interface IPortfolioRepository : IRepository<PortfolioItem>
    {
        Task<IEnumerable<PortfolioItem>> GetByCategoryAsync(string category);
    }
}