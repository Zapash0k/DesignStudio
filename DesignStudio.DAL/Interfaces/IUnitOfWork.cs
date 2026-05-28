using DesignStudio.DAL.Entities;
using DesignStudio.DAL.Interfaces;

namespace DesignStudio.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IPortfolioRepository Portfolio { get; }
        IRepository<Service> Services { get; }
        Task<int> SaveAsync();
    }
}