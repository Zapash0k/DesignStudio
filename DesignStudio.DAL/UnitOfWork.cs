using DesignStudio.DAL.Interfaces;
using DesignStudio.DAL.Repositories;
using DesignStudio.DAL.Entities;

namespace DesignStudio.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DesignStudioContext _context;

        private IOrderRepository? _orders;
        private IPortfolioRepository? _portfolio;
        private IRepository<Service>? _services;

        public UnitOfWork(DesignStudioContext context)
        {
            _context = context;
        }

        public IOrderRepository Orders
            => _orders ??= new OrderRepository(_context);

        public IPortfolioRepository Portfolio
            => _portfolio ??= new PortfolioRepository(_context);

        public IRepository<Service> Services
            => _services ??= new Repository<Service>(_context);

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }
}